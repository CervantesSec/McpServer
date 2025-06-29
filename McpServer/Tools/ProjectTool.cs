using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class ProjectTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<ProjectTool> _logger;

    public ProjectTool(CervantesApiClient apiClient, ILogger<ProjectTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get all projects from Cervantes")]
    public async Task<List<Project>> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all projects");
        var projects = await _apiClient.GetAsync<List<Project>>("api/Project", cancellationToken);
        return projects ?? new List<Project>();
    }

    [McpServerTool, Description("Get a specific project by ID")]
    public async Task<Project?> GetProjectByIdAsync(
        [Description("The project ID")] Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching project with ID: {ProjectId}", id);
        return await _apiClient.GetAsync<Project>($"api/Project/{id}", cancellationToken);
    }

    [McpServerTool, Description("Create a new project")]
    public async Task<Project?> CreateProjectAsync(
        [Description("Project name")] string name,
        [Description("Client ID")] Guid clientId,
        [Description("Project description")] string? description = null,
        [Description("Project start date (ISO 8601)")] string? startDate = null,
        [Description("Project end date (ISO 8601)")] string? endDate = null,
        [Description("Is template project")] bool template = false,
        [Description("Project status (0=Archived, 1=Active, 2=Waiting)")] int status = 1,
        [Description("Project type (0=Internal, 1=External)")] int projectType = 1,
        [Description("Project language (0=English, 1=Español)")] int language = 0,
        [Description("Project score (0=Low, 1=High)")] int score = 0,
        [Description("Findings ID")] string? findingsId = null,
        [Description("Business impact")] int businessImpact = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new project: {ProjectName}", name);
        
        var startDateTime = !string.IsNullOrEmpty(startDate) 
            ? DateTime.Parse(startDate) 
            : DateTime.UtcNow;
            
        var endDateTime = !string.IsNullOrEmpty(endDate) 
            ? DateTime.Parse(endDate) 
            : (DateTime?)null;

        var projectData = new ProjectCreateViewModel
        {
            Name = name,
            Description = description,
            StartDate = startDateTime,
            EndDate = endDateTime,
            Template = template,
            Status = (ProjectStatus)status,
            ProjectType = (ProjectType)projectType,
            Language = (Language)language,
            ClientId = clientId,
            Score = (Score)score,
            FindingsId = findingsId,
            BusinessImpact = businessImpact
        };

        return await _apiClient.PostAsync<ProjectCreateViewModel, Project>("api/Project", projectData, cancellationToken);
    }

    [McpServerTool, Description("Update an existing project")]
    public async Task<Project?> UpdateProjectAsync(
        [Description("Project ID")] Guid id,
        [Description("Project name")] string name,
        [Description("Client ID")] Guid clientId,
        [Description("Project description")] string? description = null,
        [Description("Project start date (ISO 8601)")] string? startDate = null,
        [Description("Project end date (ISO 8601)")] string? endDate = null,
        [Description("Is template project")] bool template = false,
        [Description("Project status (0=Archived, 1=Active, 2=Waiting)")] int status = 1,
        [Description("Project type (0=Internal, 1=External)")] int projectType = 1,
        [Description("Project language (0=English, 1=Español)")] int language = 0,
        [Description("Project score (0=Low, 1=High)")] int score = 0,
        [Description("Findings ID")] string? findingsId = null,
        [Description("Business impact")] int businessImpact = 0,
        [Description("Executive summary")] string? executiveSummary = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating project: {ProjectId}", id);
        
        var startDateTime = !string.IsNullOrEmpty(startDate) 
            ? DateTime.Parse(startDate) 
            : DateTime.UtcNow;
            
        var endDateTime = !string.IsNullOrEmpty(endDate) 
            ? DateTime.Parse(endDate) 
            : (DateTime?)null;

        var projectData = new ProjectEditViewModel
        {
            Id = id,
            Name = name,
            Description = description,
            StartDate = startDateTime,
            EndDate = endDateTime,
            Template = template,
            Status = (ProjectStatus)status,
            ProjectType = (ProjectType)projectType,
            Language = (Language)language,
            ClientId = clientId,
            Score = (Score)score,
            FindingsId = findingsId,
            BusinessImpact = businessImpact,
            ExecutiveSummary = executiveSummary
        };

        return await _apiClient.PutAsync<ProjectEditViewModel, Project>("api/Project", projectData, cancellationToken);
    }

    [McpServerTool, Description("Delete a project")]
    public async Task<bool> DeleteProjectAsync(
        [Description("Project ID")] Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting project: {ProjectId}", id);
        return await _apiClient.DeleteAsync($"api/Project/{id}", cancellationToken);
    }

    [McpServerTool, Description("Get projects by name")]
    public async Task<List<Project>> GetProjectsByNameAsync(
        [Description("Project name to search for")] string projectName,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching projects by name: {ProjectName}", projectName);
        var projects = await _apiClient.GetAsync<List<Project>>($"api/Project/{Uri.EscapeDataString(projectName)}", cancellationToken);
        return projects ?? new List<Project>();
    }

    [McpServerTool, Description("Get all projects for a specific client")]
    public async Task<List<Project>> GetProjectsByClientAsync(
        [Description("Client ID")] Guid clientId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching projects for client: {ClientId}", clientId);
        var projects = await _apiClient.GetAsync<List<Project>>($"api/Project/Client/{clientId}", cancellationToken);
        return projects ?? new List<Project>();
    }

    [McpServerTool, Description("Get all projects for a specific client by name")]
    public async Task<List<Project>> GetProjectsByClientNameAsync(
        [Description("Client name")] string clientName,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching projects for client name: {ClientName}", clientName);
        var projects = await _apiClient.GetAsync<List<Project>>($"api/Project/Client/{Uri.EscapeDataString(clientName)}", cancellationToken);
        return projects ?? new List<Project>();
    }

    [McpServerTool, Description("Generate executive summary for project")]
    public async Task<bool> GenerateExecutiveSummaryAsync(
        [Description("Project ID")] Guid projectId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Generating executive summary for project: {ProjectId}", projectId);
        var data = new { ProjectId = projectId };
        return await _apiClient.PostAsync("api/Project/ExecutiveSummary", data, cancellationToken);
    }

    [McpServerTool, Description("Verify user access to project")]
    public async Task<bool> VerifyUserAccessAsync(
        [Description("Project ID")] Guid projectId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Verifying user access for project: {ProjectId}", projectId);
        var result = await _apiClient.GetAsync<bool>($"api/Project/VerifyUser/{projectId}", cancellationToken);
        return result;
    }

    // Project Member Management
    [McpServerTool, Description("Get all members of a project")]
    public async Task<List<object>> GetProjectMembersAsync(
        [Description("Project ID")] Guid projectId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching members for project: {ProjectId}", projectId);
        var members = await _apiClient.GetAsync<List<object>>($"api/Project/Members/{projectId}", cancellationToken);
        return members ?? new List<object>();
    }

    [McpServerTool, Description("Add a member to a project")]
    public async Task<bool> AddProjectMemberAsync(
        [Description("Project ID")] Guid projectId,
        [Description("User ID to add as member")] string userId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding member {UserId} to project: {ProjectId}", userId, projectId);
        var memberData = new { ProjectId = projectId, UserId = userId };
        return await _apiClient.PostAsync("api/Project/Member", memberData, cancellationToken);
    }

    [McpServerTool, Description("Remove a member from a project")]
    public async Task<bool> RemoveProjectMemberAsync(
        [Description("Member ID to remove")] Guid memberId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Removing member: {MemberId}", memberId);
        return await _apiClient.DeleteAsync($"api/Project/Member/{memberId}", cancellationToken);
    }

    // Project Notes Management
    [McpServerTool, Description("Get all notes for a project")]
    public async Task<List<object>> GetProjectNotesAsync(
        [Description("Project ID")] Guid projectId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching notes for project: {ProjectId}", projectId);
        var notes = await _apiClient.GetAsync<List<object>>($"api/Project/Note/{projectId}", cancellationToken);
        return notes ?? new List<object>();
    }

    [McpServerTool, Description("Add a note to a project")]
    public async Task<bool> AddProjectNoteAsync(
        [Description("Project ID")] Guid projectId,
        [Description("Note name")] string name,
        [Description("Note description")] string description,
        [Description("Note visibility (0=Private, 1=Public)")] int visibility = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding note to project: {ProjectId}", projectId);
        var noteData = new { 
            ProjectId = projectId, 
            Name = name, 
            Description = description, 
            Visibility = visibility 
        };
        return await _apiClient.PostAsync("api/Project/Note", noteData, cancellationToken);
    }

    [McpServerTool, Description("Delete a project note")]
    public async Task<bool> DeleteProjectNoteAsync(
        [Description("Note ID to delete")] Guid noteId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting project note: {NoteId}", noteId);
        return await _apiClient.DeleteAsync($"api/Project/Note/{noteId}", cancellationToken);
    }

    // Project Attachments Management
    [McpServerTool, Description("Get all attachments for a project")]
    public async Task<List<object>> GetProjectAttachmentsAsync(
        [Description("Project ID")] Guid projectId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching attachments for project: {ProjectId}", projectId);
        var attachments = await _apiClient.GetAsync<List<object>>($"api/Project/Attachment/{projectId}", cancellationToken);
        return attachments ?? new List<object>();
    }

    [McpServerTool, Description("Upload an attachment to a project")]
    public async Task<bool> AddProjectAttachmentAsync(
        [Description("Project ID")] Guid projectId,
        [Description("File name")] string fileName,
        [Description("File content as base64 string")] string fileContentBase64,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding attachment to project: {ProjectId}", projectId);
        
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

        var attachmentData = new { 
            ProjectId = projectId, 
            FileName = fileName, 
            FileContent = fileContent 
        };
        return await _apiClient.PostAsync("api/Project/Attachment", attachmentData, cancellationToken);
    }

    [McpServerTool, Description("Delete a project attachment")]
    public async Task<bool> DeleteProjectAttachmentAsync(
        [Description("Attachment ID to delete")] Guid attachmentId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting project attachment: {AttachmentId}", attachmentId);
        return await _apiClient.DeleteAsync($"api/Project/Attachment/{attachmentId}", cancellationToken);
    }
}