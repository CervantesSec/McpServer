using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class TargetTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<TargetTool> _logger;

    public TargetTool(CervantesApiClient apiClient, ILogger<TargetTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get all targets")]
    public async Task<List<Target>> GetTargetsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all targets");
        var targets = await _apiClient.GetAsync<List<Target>>("api/Target", cancellationToken);
        return targets ?? new List<Target>();
    }

    [McpServerTool, Description("Get a specific target by ID")]
    public async Task<Target?> GetTargetByIdAsync(
        [Description("Target ID")] Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching target with ID: {TargetId}", id);
        return await _apiClient.GetAsync<Target>($"api/Target/{id}", cancellationToken);
    }

    [McpServerTool, Description("Create a new target")]
    public async Task<Target?> CreateTargetAsync(
        [Description("Target name")] string name,
        [Description("Project ID")] Guid projectId,
        [Description("Target type (0=Domain, 1=Ip, 2=Binary, 3=CIDR, 4=Hostname)")] int type,
        [Description("Target description")] string? description = null,
        [Description("User ID")] string? userId = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new target: {TargetName}", name);
        
        var targetData = new TargetCreateViewModel
        {
            Name = name,
            Description = description,
            Type = (TargetType)type,
            ProjectId = projectId,
            UserId = userId
        };

        return await _apiClient.PostAsync<TargetCreateViewModel, Target>("api/Target", targetData, cancellationToken);
    }

    [McpServerTool, Description("Update an existing target")]
    public async Task<Target?> UpdateTargetAsync(
        [Description("Target ID")] Guid id,
        [Description("Target name")] string? name = null,
        [Description("Target description")] string? description = null,
        [Description("Target type (0=Domain, 1=Ip, 2=Binary, 3=CIDR, 4=Hostname)")] int type = 0,
        [Description("User ID")] string? userId = null,
        [Description("Project ID")] Guid? projectId = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating target: {TargetId}", id);
        
        var targetData = new TargetEditViewModel
        {
            Id = id,
            Name = name,
            Description = description,
            Type = (TargetType)type,
            UserId = userId,
            ProjectId = projectId
        };

        return await _apiClient.PutAsync<TargetEditViewModel, Target>("api/Target", targetData, cancellationToken);
    }

    [McpServerTool, Description("Delete a target")]
    public async Task<bool> DeleteTargetAsync(
        [Description("Target ID")] Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting target: {TargetId}", id);
        return await _apiClient.DeleteAsync($"api/Target/{id}", cancellationToken);
    }

    [McpServerTool, Description("Get targets for a specific project")]
    public async Task<List<Target>> GetTargetsByProjectAsync(
        [Description("Project ID")] Guid projectId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching targets for project: {ProjectId}", projectId);
        var targets = await _apiClient.GetAsync<List<Target>>($"api/Target/Project/{projectId}", cancellationToken);
        return targets ?? new List<Target>();
    }

    [McpServerTool, Description("Get services for a specific target")]
    public async Task<List<TargetServices>> GetTargetServicesAsync(
        [Description("Target ID")] Guid targetId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching services for target: {TargetId}", targetId);
        var services = await _apiClient.GetAsync<List<TargetServices>>($"api/Target/{targetId}/Services", cancellationToken);
        return services ?? new List<TargetServices>();
    }

    [McpServerTool, Description("Add a service to a target")]
    public async Task<TargetServices?> AddTargetServiceAsync(
        [Description("Target ID")] Guid targetId,
        [Description("Service name")] string name,
        [Description("Service port")] int port,
        [Description("Service version")] string? version = null,
        [Description("Service note")] string? note = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding service to target: {TargetId}", targetId);
        
        var serviceData = new
        {
            targetId,
            name,
            port,
            version,
            note
        };

        return await _apiClient.PostAsync<object, TargetServices>($"api/Target/{targetId}/Services", serviceData, cancellationToken);
    }

    [McpServerTool, Description("Delete a service from a target")]
    public async Task<bool> DeleteTargetServiceAsync(
        [Description("Target ID")] Guid targetId,
        [Description("Service ID")] Guid serviceId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting service {ServiceId} from target: {TargetId}", serviceId, targetId);
        return await _apiClient.DeleteAsync($"api/Target/{targetId}/Services/{serviceId}", cancellationToken);
    }

    [McpServerTool, Description("Import targets from external source")]
    public async Task<bool> ImportTargetsAsync(
        [Description("Project ID")] Guid projectId,
        [Description("Import data as JSON string")] string importData,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Importing targets for project: {ProjectId}", projectId);
        var data = new { projectId, importData };
        return await _apiClient.PostAsync($"api/Target/Import", data, cancellationToken);
    }
}