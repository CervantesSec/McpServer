using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class ReportTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<ReportTool> _logger;

    public ReportTool(CervantesApiClient apiClient, ILogger<ReportTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get all reports")]
    public async Task<List<object>> GetReportsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all reports");
        var reports = await _apiClient.GetAsync<List<object>>("api/Report", cancellationToken);
        return reports ?? new List<object>();
    }

    [McpServerTool, Description("Update an existing report")]
    public async Task<object?> UpdateReportAsync(
        [Description("Report ID")] Guid id,
        [Description("Report name")] string name,
        [Description("Report description")] string? description = null,
        [Description("Report language (0=English, 1=Español)")] int language = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating report: {ReportId}", id);
        
        var reportData = new
        {
            id,
            name,
            description,
            language
        };

        return await _apiClient.PutAsync<object, object>("api/Report", reportData, cancellationToken);
    }

    [McpServerTool, Description("Get all reports for a specific project")]
    public async Task<List<object>> GetReportsByProjectAsync(
        [Description("Project ID")] Guid projectId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching reports for project: {ProjectId}", projectId);
        var reports = await _apiClient.GetAsync<List<object>>($"api/Report/Project/{projectId}", cancellationToken);
        return reports ?? new List<object>();
    }

    [McpServerTool, Description("Generate a new report")]
    public async Task<object?> GenerateNewReportAsync(
        [Description("Report name")] string name,
        [Description("Project ID")] Guid projectId,
        [Description("Report description")] string? description = null,
        [Description("Report language (0=English, 1=Español)")] int language = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Generating new report: {ReportName}", name);
        
        var reportData = new
        {
            name,
            description,
            language,
            projectId
        };

        return await _apiClient.PostAsync<object, object>("api/Report/GenerateNewReport", reportData, cancellationToken);
    }

    [McpServerTool, Description("Delete a report")]
    public async Task<bool> DeleteReportAsync(
        [Description("Report ID")] Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting report: {ReportId}", id);
        return await _apiClient.DeleteAsync($"api/Report/{id}", cancellationToken);
    }

    [McpServerTool, Description("Get all report templates")]
    public async Task<List<object>> GetReportTemplatesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching report templates");
        var templates = await _apiClient.GetAsync<List<object>>("api/Report/Templates", cancellationToken);
        return templates ?? new List<object>();
    }

    [McpServerTool, Description("Create a new report template")]
    public async Task<object?> CreateReportTemplateAsync(
        [Description("Template name")] string name,
        [Description("Template description")] string? description = null,
        [Description("Template language (0=English, 1=Español)")] int language = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new report template: {TemplateName}", name);
        
        var templateData = new
        {
            name,
            description,
            language
        };

        return await _apiClient.PostAsync<object, object>("api/Report/Template", templateData, cancellationToken);
    }

    [McpServerTool, Description("Update an existing report template")]
    public async Task<object?> UpdateReportTemplateAsync(
        [Description("Template ID")] Guid id,
        [Description("Template name")] string name,
        [Description("Template description")] string? description = null,
        [Description("Template language (0=English, 1=Español)")] int language = 0,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating report template: {TemplateId}", id);
        
        var templateData = new
        {
            id,
            name,
            description,
            language
        };

        return await _apiClient.PutAsync<object, object>("api/Report/Template", templateData, cancellationToken);
    }

    [McpServerTool, Description("Delete a report template")]
    public async Task<bool> DeleteReportTemplateAsync(
        [Description("Template ID")] Guid templateId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting report template: {TemplateId}", templateId);
        return await _apiClient.DeleteAsync($"api/Report/Template/{templateId}", cancellationToken);
    }

    [McpServerTool, Description("Get report components")]
    public async Task<List<object>> GetReportComponentsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching report components");
        var components = await _apiClient.GetAsync<List<object>>("api/Report/Components", cancellationToken);
        return components ?? new List<object>();
    }

    [McpServerTool, Description("Get report parts for a specific template")]
    public async Task<List<object>> GetReportPartsAsync(
        [Description("Template ID")] Guid templateId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching report parts for template: {TemplateId}", templateId);
        var parts = await _apiClient.GetAsync<List<object>>($"api/Report/Parts/{templateId}", cancellationToken);
        return parts ?? new List<object>();
    }

    [McpServerTool, Description("Delete a report component")]
    public async Task<bool> DeleteReportComponentAsync(
        [Description("Component ID")] Guid componentId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting report component: {ComponentId}", componentId);
        return await _apiClient.DeleteAsync($"api/Report/Components/{componentId}", cancellationToken);
    }

    [McpServerTool, Description("Download a generated report")]
    public async Task<byte[]?> DownloadReportAsync(
        [Description("Report ID")] Guid reportId,
        [Description("Report format (pdf, docx, html)")] string format = "pdf",
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Downloading report: {ReportId} in format: {Format}", reportId, format);
        
        var downloadData = new
        {
            reportId,
            format
        };
        
        return await _apiClient.PostAsync<object, byte[]>("api/Report/Download", downloadData, cancellationToken);
    }

    [McpServerTool, Description("Import a report from external source")]
    public async Task<bool> ImportReportAsync(
        [Description("Project ID")] Guid projectId,
        [Description("Import data as JSON string")] string importData,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Importing report for project: {ProjectId}", projectId);
        var data = new { projectId, importData };
        return await _apiClient.PostAsync($"api/Report/Import", data, cancellationToken);
    }

}