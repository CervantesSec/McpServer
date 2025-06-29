using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class KnowledgeBaseTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<KnowledgeBaseTool> _logger;

    public KnowledgeBaseTool(CervantesApiClient apiClient, ILogger<KnowledgeBaseTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    // Knowledge Base Pages
    [McpServerTool, Description("Get all knowledge base pages")]
    public async Task<List<KnowledgeBase>> GetKnowledgePagesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all knowledge base pages");
        var pages = await _apiClient.GetAsync<List<KnowledgeBase>>("api/KnowledgeBase/Page", cancellationToken);
        return pages ?? new List<KnowledgeBase>();
    }

    [McpServerTool, Description("Create a new knowledge base page")]
    public async Task<KnowledgeBase?> CreateKnowledgePageAsync(
        [Description("Title of the page")] string title,
        [Description("Content of the page")] string content,
        [Description("Category ID")] Guid categoryId,
        [Description("Display order")] int order = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new knowledge base page: {PageTitle}", title);
        
        var pageData = new KnowledgePageCreateViewModel
        {
            Title = title,
            Content = content,
            CategoryId = categoryId,
            Order = order
        };

        return await _apiClient.PostAsync<KnowledgePageCreateViewModel, KnowledgeBase>("api/KnowledgeBase/Page", pageData, cancellationToken);
    }

    [McpServerTool, Description("Update an existing knowledge base page")]
    public async Task<KnowledgeBase?> UpdateKnowledgePageAsync(
        [Description("Page ID")] Guid id,
        [Description("Title of the page")] string title,
        [Description("Content of the page")] string content,
        [Description("Category ID")] Guid categoryId,
        [Description("Display order")] int order = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating knowledge base page: {PageId}", id);
        
        var pageData = new KnowledgePageEditViewModel
        {
            Id = id,
            Title = title,
            Content = content,
            CategoryId = categoryId,
            Order = order
        };

        return await _apiClient.PutAsync<KnowledgePageEditViewModel, KnowledgeBase>("api/KnowledgeBase/Page", pageData, cancellationToken);
    }

    [McpServerTool, Description("Delete a knowledge base page")]
    public async Task<bool> DeleteKnowledgePageAsync(
        [Description("Page ID")] Guid pageId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting knowledge base page: {PageId}", pageId);
        return await _apiClient.DeleteAsync($"api/KnowledgeBase/Page/{pageId}", cancellationToken);
    }

    // Knowledge Base Categories
    [McpServerTool, Description("Get all knowledge base categories")]
    public async Task<List<KnowledgeBaseCategories>> GetKnowledgeCategoriesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all knowledge base categories");
        var categories = await _apiClient.GetAsync<List<KnowledgeBaseCategories>>("api/KnowledgeBase/Category", cancellationToken);
        return categories ?? new List<KnowledgeBaseCategories>();
    }

    [McpServerTool, Description("Create a new knowledge base category")]
    public async Task<KnowledgeBaseCategories?> CreateKnowledgeCategoryAsync(
        [Description("Name of the category")] string name,
        [Description("Description of the category")] string? description = null,
        [Description("Icon for the category")] string? icon = null,
        [Description("Display order")] int order = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new knowledge base category: {CategoryName}", name);
        
        var categoryData = new KnowledgeBaseCategoryCreateViewModel
        {
            Name = name,
            Description = description,
            Icon = icon,
            Order = order
        };

        return await _apiClient.PostAsync<KnowledgeBaseCategoryCreateViewModel, KnowledgeBaseCategories>("api/KnowledgeBase/Category", categoryData, cancellationToken);
    }

    [McpServerTool, Description("Update an existing knowledge base category")]
    public async Task<KnowledgeBaseCategories?> UpdateKnowledgeCategoryAsync(
        [Description("Category ID")] Guid id,
        [Description("Name of the category")] string name,
        [Description("Description of the category")] string? description = null,
        [Description("Icon for the category")] string? icon = null,
        [Description("Display order")] int order = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating knowledge base category: {CategoryId}", id);
        
        var categoryData = new KnowledgeBaseCategoryEditViewModel
        {
            Id = id,
            Name = name,
            Description = description,
            Icon = icon,
            Order = order
        };

        return await _apiClient.PutAsync<KnowledgeBaseCategoryEditViewModel, KnowledgeBaseCategories>("api/KnowledgeBase/Category", categoryData, cancellationToken);
    }

    [McpServerTool, Description("Delete a knowledge base category")]
    public async Task<bool> DeleteKnowledgeCategoryAsync(
        [Description("Category ID")] Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting knowledge base category: {CategoryId}", categoryId);
        return await _apiClient.DeleteAsync($"api/KnowledgeBase/Category/{categoryId}", cancellationToken);
    }
}