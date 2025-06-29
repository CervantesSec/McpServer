using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class ClientTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<ClientTool> _logger;

    public ClientTool(CervantesApiClient apiClient, ILogger<ClientTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get all clients from Cervantes")]
    public async Task<List<Client>> GetClientsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all clients");
        var clients = await _apiClient.GetAsync<List<Client>>("api/Clients", cancellationToken);
        return clients ?? new List<Client>();
    }

    [McpServerTool, Description("Get a specific client by ID")]
    public async Task<Client?> GetClientByIdAsync(
        [Description("The client ID")] Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching client with ID: {ClientId}", id);
        return await _apiClient.GetAsync<Client>($"api/Clients/{id}", cancellationToken);
    }

    [McpServerTool, Description("Create a new client")]
    public async Task<Client?> CreateClientAsync(
        [Description("Client name")] string name,
        [Description("Client description")] string? description = null,
        [Description("Client URL")] string? url = null,
        [Description("Contact person name")] string? contactName = null,
        [Description("Contact email")] string? contactEmail = null,
        [Description("Contact phone")] string? contactPhone = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new client: {ClientName}", name);
        
        var clientData = new ClientCreateViewModel
        {
            Name = name,
            Description = description,
            Url = url,
            ContactName = contactName,
            ContactEmail = contactEmail,
            ContactPhone = contactPhone
        };

        return await _apiClient.PostAsync<ClientCreateViewModel, Client>("api/Clients", clientData, cancellationToken);
    }

    [McpServerTool, Description("Update an existing client")]
    public async Task<Client?> UpdateClientAsync(
        [Description("Client ID")] Guid id,
        [Description("Client name")] string name,
        [Description("Client description")] string? description = null,
        [Description("Client URL")] string? url = null,
        [Description("Contact person name")] string? contactName = null,
        [Description("Contact email")] string? contactEmail = null,
        [Description("Contact phone")] string? contactPhone = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating client: {ClientId}", id);
        
        var clientData = new ClientEditViewModel
        {
            Id = id,
            Name = name,
            Description = description,
            Url = url,
            ContactName = contactName,
            ContactEmail = contactEmail,
            ContactPhone = contactPhone
        };

        return await _apiClient.PutAsync<ClientEditViewModel, Client>("api/Clients", clientData, cancellationToken);
    }

    [McpServerTool, Description("Delete a client")]
    public async Task<bool> DeleteClientAsync(
        [Description("Client ID")] Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting client: {ClientId}", id);
        return await _apiClient.DeleteAsync($"api/Clients/{id}", cancellationToken);
    }

    [McpServerTool, Description("Search clients by name")]
    public async Task<List<Client>> SearchClientsAsync(
        [Description("Client name to search for")] string name,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching clients by name: {Name}", name);
        var clients = await _apiClient.GetAsync<List<Client>>($"api/Clients/{Uri.EscapeDataString(name)}", cancellationToken);
        return clients ?? new List<Client>();
    }

    [McpServerTool, Description("Delete a client avatar")]
    public async Task<bool> DeleteClientAvatarAsync(
        [Description("Client ID")] Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting avatar for client: {ClientId}", id);
        return await _apiClient.DeleteAsync($"api/Clients/Avatar/{id}", cancellationToken);
    }
}