using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class VaultTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<VaultTool> _logger;

    public VaultTool(CervantesApiClient apiClient, ILogger<VaultTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get vault entries for a specific project")]
    public async Task<List<Vault>> GetVaultByProjectAsync(
        [Description("Project ID")] Guid projectId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching vault entries for project: {ProjectId}", projectId);
        var vaults = await _apiClient.GetAsync<List<Vault>>($"api/Vault/Project/{projectId}", cancellationToken);
        return vaults ?? new List<Vault>();
    }

    [McpServerTool, Description("Create a new vault entry")]
    public async Task<Vault?> CreateVaultEntryAsync(
        [Description("Name of the vault entry")] string name,
        [Description("Project ID")] Guid projectId,
        [Description("Vault type (0=Credential, 1=Note, 2=Identity, 3=Card, 4=SecureNote, 5=Other)")] int type,
        [Description("Value/content of the vault entry")] string value,
        [Description("Description of the vault entry")] string? description = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new vault entry: {VaultName} for project: {ProjectId}", name, projectId);
        
        var vaultData = new VaultCreateViewModel
        {
            Name = name,
            ProjectId = projectId,
            Type = (VaultType)type,
            Value = value,
            Description = description
        };

        return await _apiClient.PostAsync<VaultCreateViewModel, Vault>("api/Vault", vaultData, cancellationToken);
    }

    [McpServerTool, Description("Update an existing vault entry")]
    public async Task<Vault?> UpdateVaultEntryAsync(
        [Description("Vault entry ID")] Guid id,
        [Description("Name of the vault entry")] string name,
        [Description("Project ID")] Guid projectId,
        [Description("Vault type (0=Credential, 1=Note, 2=Identity, 3=Card, 4=SecureNote, 5=Other)")] int type,
        [Description("Value/content of the vault entry")] string value,
        [Description("Description of the vault entry")] string? description = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating vault entry: {VaultId}", id);
        
        var vaultData = new VaultEditViewModel
        {
            Id = id,
            Name = name,
            ProjectId = projectId,
            Type = (VaultType)type,
            Value = value,
            Description = description
        };

        return await _apiClient.PutAsync<VaultEditViewModel, Vault>("api/Vault", vaultData, cancellationToken);
    }

    [McpServerTool, Description("Delete a vault entry")]
    public async Task<bool> DeleteVaultEntryAsync(
        [Description("Vault entry ID")] Guid vaultId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting vault entry: {VaultId}", vaultId);
        return await _apiClient.DeleteAsync($"api/Vault/{vaultId}", cancellationToken);
    }
}