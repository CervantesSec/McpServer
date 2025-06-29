using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class TaskTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<TaskTool> _logger;

    public TaskTool(CervantesApiClient apiClient, ILogger<TaskTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get all tasks")]
    public async Task<List<Models.Task>> GetTasksAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all tasks");
        var tasks = await _apiClient.GetAsync<List<Models.Task>>("api/Task", cancellationToken);
        return tasks ?? new List<Models.Task>();
    }

    [McpServerTool, Description("Get a specific task by ID")]
    public async Task<Models.Task?> GetTaskByIdAsync(
        [Description("Task ID")] Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching task with ID: {TaskId}", id);
        return await _apiClient.GetAsync<Models.Task>($"api/Task/{id}", cancellationToken);
    }

    [McpServerTool, Description("Create a new task")]
    public async Task<Models.Task?> CreateTaskAsync(
        [Description("Task name")] string? name = null,
        [Description("Task description")] string? description = null,
        [Description("Start date (ISO 8601)")] string? startDate = null,
        [Description("End date (ISO 8601)")] string? endDate = null,
        [Description("Status (0=Waiting, 1=InProgress, 2=Blocked, 3=Ready, 4=Completed)")] int status = 0,
        [Description("Created user ID")] string? createdUserId = null,
        [Description("Assigned user ID")] string? assignedUserId = null,
        [Description("Project ID")] Guid? projectId = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new task: {TaskName}", name);
        
        var startDateTime = !string.IsNullOrEmpty(startDate) 
            ? DateTime.Parse(startDate) 
            : DateTime.UtcNow;
            
        var endDateTime = !string.IsNullOrEmpty(endDate) 
            ? DateTime.Parse(endDate) 
            : DateTime.UtcNow;

        var taskData = new TaskCreateViewModel
        {
            CreatedUserId = createdUserId,
            AsignedUserId = assignedUserId,
            ProjectId = projectId,
            StartDate = startDateTime,
            EndDate = endDateTime,
            Name = name,
            Description = description,
            Status = (Models.TaskStatus)status
        };

        return await _apiClient.PostAsync<TaskCreateViewModel, Models.Task>("api/Task", taskData, cancellationToken);
    }

    [McpServerTool, Description("Delete a task")]
    public async Task<bool> DeleteTaskAsync(
        [Description("Task ID")] Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting task: {TaskId}", id);
        return await _apiClient.DeleteAsync($"api/Task/{id}", cancellationToken);
    }

    [McpServerTool, Description("Get tasks for a specific project")]
    public async Task<List<Models.Task>> GetTasksByProjectAsync(
        [Description("Project ID")] Guid projectId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching tasks for project: {ProjectId}", projectId);
        var tasks = await _apiClient.GetAsync<List<Models.Task>>($"api/Task/Project/{projectId}", cancellationToken);
        return tasks ?? new List<Models.Task>();
    }

    [McpServerTool, Description("Get tasks for a specific client")]
    public async Task<List<Models.Task>> GetTasksByClientAsync(
        [Description("Client ID")] Guid clientId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching tasks for client: {ClientId}", clientId);
        var tasks = await _apiClient.GetAsync<List<Models.Task>>($"api/Task/Client/{clientId}", cancellationToken);
        return tasks ?? new List<Models.Task>();
    }

    [McpServerTool, Description("Get tasks for a project assigned to current user")]
    public async Task<List<Models.Task>> GetTasksByProjectUserAsync(
        [Description("Project ID")] Guid projectId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching tasks for project assigned to user: {ProjectId}", projectId);
        var tasks = await _apiClient.GetAsync<List<Models.Task>>($"api/Task/Project/{projectId}/User", cancellationToken);
        return tasks ?? new List<Models.Task>();
    }

    [McpServerTool, Description("Update an existing task status")]
    public async Task<Models.Task?> UpdateTaskAsync(
        [Description("Task ID")] Guid id,
        [Description("Status (0=Waiting, 1=InProgress, 2=Blocked, 3=Ready, 4=Completed)")] int status = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating task: {TaskId}", id);

        var taskData = new TaskUpdateViewModel
        {
            Id = id,
            Status = (Models.TaskStatus)status
        };

        return await _apiClient.PostAsync<TaskUpdateViewModel, Models.Task>("api/Task/Update", taskData, cancellationToken);
    }

    [McpServerTool, Description("Edit a task with full details")]
    public async Task<Models.Task?> EditTaskAsync(
        [Description("Task ID")] Guid id,
        [Description("Task name")] string? name = null,
        [Description("Task description")] string? description = null,
        [Description("Start date (ISO 8601)")] string? startDate = null,
        [Description("End date (ISO 8601)")] string? endDate = null,
        [Description("Status (0=Waiting, 1=InProgress, 2=Blocked, 3=Ready, 4=Completed)")] int status = 0,
        [Description("Created user ID")] string? createdUserId = null,
        [Description("Assigned user ID")] string? assignedUserId = null,
        [Description("Project ID")] Guid? projectId = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Editing task: {TaskId}", id);
        
        var startDateTime = !string.IsNullOrEmpty(startDate) 
            ? DateTime.Parse(startDate) 
            : DateTime.UtcNow;
            
        var endDateTime = !string.IsNullOrEmpty(endDate) 
            ? DateTime.Parse(endDate) 
            : DateTime.UtcNow;

        var taskData = new TaskEditViewModel
        {
            Id = id,
            CreatedUserId = createdUserId,
            AsignedUserId = assignedUserId,
            ProjectId = projectId,
            StartDate = startDateTime,
            EndDate = endDateTime,
            Name = name,
            Description = description,
            Status = (Models.TaskStatus)status
        };

        return await _apiClient.PutAsync<TaskEditViewModel, Models.Task>("api/Task", taskData, cancellationToken);
    }

    // Task Notes Management
    [McpServerTool, Description("Get all notes for a task")]
    public async Task<List<object>> GetTaskNotesAsync(
        [Description("Task ID")] Guid taskId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching notes for task: {TaskId}", taskId);
        var notes = await _apiClient.GetAsync<List<object>>($"api/Task/Notes/{taskId}", cancellationToken);
        return notes ?? new List<object>();
    }

    [McpServerTool, Description("Add a note to a task")]
    public async Task<bool> AddTaskNoteAsync(
        [Description("Task ID")] Guid taskId,
        [Description("Note name")] string name,
        [Description("Note description")] string description,
        [Description("Note visibility (0=Private, 1=Public)")] int visibility = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding note to task: {TaskId}", taskId);
        
        var noteData = new TaskNoteViewModel
        {
            TaskId = taskId,
            Name = name,
            Description = description,
            Visibility = (Visibility)visibility
        };

        return await _apiClient.PostAsync("api/Task/Notes", noteData, cancellationToken);
    }

    // Task Targets Management
    [McpServerTool, Description("Get all targets for a task")]
    public async Task<List<object>> GetTaskTargetsAsync(
        [Description("Task ID")] Guid taskId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching targets for task: {TaskId}", taskId);
        var targets = await _apiClient.GetAsync<List<object>>($"api/Task/Targets/{taskId}", cancellationToken);
        return targets ?? new List<object>();
    }

    [McpServerTool, Description("Add a target to a task")]
    public async Task<bool> AddTaskTargetAsync(
        [Description("Task ID")] Guid taskId,
        [Description("Target ID")] Guid targetId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding target {TargetId} to task: {TaskId}", targetId, taskId);
        
        var targetData = new TaskTargetViewModel
        {
            TaskId = taskId,
            TargetId = targetId
        };

        return await _apiClient.PostAsync("api/Task/Target", targetData, cancellationToken);
    }

    [McpServerTool, Description("Remove a target from a task")]
    public async Task<bool> RemoveTaskTargetAsync(
        [Description("Task target ID")] Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Removing task target: {TaskTargetId}", id);
        return await _apiClient.DeleteAsync($"api/Task/Target/{id}", cancellationToken);
    }

    // Task Attachments Management
    [McpServerTool, Description("Get all attachments for a task")]
    public async Task<List<object>> GetTaskAttachmentsAsync(
        [Description("Task ID")] Guid taskId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching attachments for task: {TaskId}", taskId);
        var attachments = await _apiClient.GetAsync<List<object>>($"api/Task/Attachments/{taskId}", cancellationToken);
        return attachments ?? new List<object>();
    }

    [McpServerTool, Description("Add an attachment to a task")]
    public async Task<bool> AddTaskAttachmentAsync(
        [Description("Task ID")] Guid taskId,
        [Description("Attachment name")] string name,
        [Description("File name")] string? fileName = null,
        [Description("File content as base64 string")] string? fileContentBase64 = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding attachment to task: {TaskId}", taskId);
        
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

        var attachmentData = new TaskAttachmentViewModel
        {
            TaskId = taskId,
            Name = name,
            FileName = fileName,
            FileContent = fileContent
        };

        return await _apiClient.PostAsync("api/Task/Attachments", attachmentData, cancellationToken);
    }
}