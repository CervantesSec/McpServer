using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class NoteTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<NoteTool> _logger;

    public NoteTool(CervantesApiClient apiClient, ILogger<NoteTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get all notes")]
    public async Task<List<Note>> GetNotesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all notes");
        var notes = await _apiClient.GetAsync<List<Note>>("api/Note", cancellationToken);
        return notes ?? new List<Note>();
    }

    [McpServerTool, Description("Create a new note")]
    public async Task<Note?> CreateNoteAsync(
        [Description("Name of the note")] string name,
        [Description("Description/content of the note")] string? description = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new note: {NoteName}", name);
        
        var noteData = new NoteCreateViewModel
        {
            Name = name,
            Description = description
        };

        return await _apiClient.PostAsync<NoteCreateViewModel, Note>("api/Note", noteData, cancellationToken);
    }

    [McpServerTool, Description("Update an existing note")]
    public async Task<Note?> UpdateNoteAsync(
        [Description("Note ID")] Guid id,
        [Description("Name of the note")] string name,
        [Description("Description/content of the note")] string? description = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating note: {NoteId}", id);
        
        var noteData = new NoteEditViewModel
        {
            Id = id,
            Name = name,
            Description = description
        };

        return await _apiClient.PutAsync<NoteEditViewModel, Note>("api/Note", noteData, cancellationToken);
    }

    [McpServerTool, Description("Delete a note")]
    public async Task<bool> DeleteNoteAsync(
        [Description("Note ID")] Guid noteId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting note: {NoteId}", noteId);
        return await _apiClient.DeleteAsync($"api/Note/{noteId}", cancellationToken);
    }
}