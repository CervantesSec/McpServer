using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class RoleTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<RoleTool> _logger;

    public RoleTool(CervantesApiClient apiClient, ILogger<RoleTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get all roles with their permissions")]
    public async Task<List<RoleWithPermissionNamesDto>> GetRolesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all roles");
        var roles = await _apiClient.GetAsync<List<RoleWithPermissionNamesDto>>("api/User/Roles", cancellationToken);
        return roles ?? new List<RoleWithPermissionNamesDto>();
    }

    [McpServerTool, Description("Get role by user ID")]
    public async Task<string?> GetRoleByUserIdAsync(
        [Description("User ID")] string userId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching role for user: {UserId}", userId);
        return await _apiClient.GetAsync<string>($"api/User/Role/{userId}", cancellationToken);
    }

    [McpServerTool, Description("Get role by role name")]
    public async Task<RoleWithPermissionNamesDto?> GetRoleByNameAsync(
        [Description("Role name")] string roleName,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching role: {RoleName}", roleName);
        return await _apiClient.GetAsync<RoleWithPermissionNamesDto>($"api/User/Role/{roleName}", cancellationToken);
    }

    [McpServerTool, Description("Create a new role")]
    public async Task<RoleWithPermissionNamesDto?> CreateRoleAsync(
        [Description("Role name")] string name,
        [Description("Role description")] string? description = null,
        [Description("List of permission names")] List<string>? permissions = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new role: {RoleName}", name);
        
        var roleData = new CreateRoleViewModel
        {
            Name = name,
            Description = description,
            Permissions = permissions ?? new List<string>()
        };

        return await _apiClient.PostAsync<CreateRoleViewModel, RoleWithPermissionNamesDto>("api/User/Role", roleData, cancellationToken);
    }

    [McpServerTool, Description("Update an existing role")]
    public async Task<RoleWithPermissionNamesDto?> UpdateRoleAsync(
        [Description("Role name")] string roleName,
        [Description("Role description")] string? description = null,
        [Description("List of permission names")] List<string>? permissions = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating role: {RoleName}", roleName);
        
        var roleData = new CreateRoleViewModel
        {
            Name = roleName,
            Description = description,
            Permissions = permissions ?? new List<string>()
        };

        return await _apiClient.PutAsync<CreateRoleViewModel, RoleWithPermissionNamesDto>("api/User/Role", roleData, cancellationToken);
    }

    [McpServerTool, Description("Delete a role")]
    public async Task<bool> DeleteRoleAsync(
        [Description("Role name")] string roleName,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting role: {RoleName}", roleName);
        return await _apiClient.DeleteAsync($"api/User/Role/{roleName}", cancellationToken);
    }
}