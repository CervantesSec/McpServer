using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class DocumentTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<DocumentTool> _logger;

    public DocumentTool(CervantesApiClient apiClient, ILogger<DocumentTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get all documents")]
    public async Task<List<Document>> GetDocumentsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all documents");
        var documents = await _apiClient.GetAsync<List<Document>>("api/Document", cancellationToken);
        return documents ?? new List<Document>();
    }

    [McpServerTool, Description("Create a new document")]
    public async Task<Document?> CreateDocumentAsync(
        [Description("Name of the document")] string name,
        [Description("Description of the document")] string? description = null,
        [Description("File name")] string? fileName = null,
        [Description("File content as base64 string")] string? fileContentBase64 = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new document: {DocumentName}", name);
        
        byte[]? fileContent = null;
        if (!string.IsNullOrEmpty(fileContentBase64))
        {
            try
            {
                fileContent = Convert.FromBase64String(fileContentBase64);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, "Invalid base64 format for file content");
                throw new ArgumentException("Invalid base64 format for file content", nameof(fileContentBase64));
            }
        }

        var documentData = new DocumentCreateViewModel
        {
            Name = name,
            Description = description,
            FileName = fileName,
            FileContent = fileContent
        };

        return await _apiClient.PostAsync<DocumentCreateViewModel, Document>("api/Document", documentData, cancellationToken);
    }

    [McpServerTool, Description("Update an existing document")]
    public async Task<Document?> UpdateDocumentAsync(
        [Description("Document ID")] Guid id,
        [Description("Name of the document")] string name,
        [Description("Description of the document")] string? description = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating document: {DocumentId}", id);
        
        var documentData = new DocumentEditViewModel
        {
            Id = id,
            Name = name,
            Description = description
        };

        return await _apiClient.PutAsync<DocumentEditViewModel, Document>("api/Document", documentData, cancellationToken);
    }

    [McpServerTool, Description("Delete a document")]
    public async Task<bool> DeleteDocumentAsync(
        [Description("Document ID")] Guid docId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting document: {DocumentId}", docId);
        return await _apiClient.DeleteAsync($"api/Document/{docId}", cancellationToken);
    }
}