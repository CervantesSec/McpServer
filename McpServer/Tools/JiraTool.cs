using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class JiraTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<JiraTool> _logger;

    public JiraTool(CervantesApiClient apiClient, ILogger<JiraTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get all JIRA issues")]
    public async Task<List<Jira>> GetJiraIssuesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all JIRA issues");
        var issues = await _apiClient.GetAsync<List<Jira>>("api/Jira", cancellationToken);
        return issues ?? new List<Jira>();
    }

    [McpServerTool, Description("Get JIRA issue for a specific vulnerability")]
    public async Task<Jira?> GetJiraIssueByVulnAsync(
        [Description("Vulnerability ID")] Guid vulnId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching JIRA issue for vulnerability: {VulnId}", vulnId);
        return await _apiClient.GetAsync<Jira>($"api/Jira/{vulnId}", cancellationToken);
    }

    [McpServerTool, Description("Create a JIRA issue for a vulnerability")]
    public async Task<Jira?> CreateJiraIssueAsync(
        [Description("Vulnerability ID")] Guid vulnId,
        [Description("JIRA issue name/summary")] string name,
        [Description("JIRA reporter")] string? reporter = null,
        [Description("JIRA assignee")] string? assignee = null,
        [Description("JIRA issue type")] string? jiraType = null,
        [Description("JIRA labels")] string? label = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating JIRA issue for vulnerability: {VulnId}", vulnId);
        
        var jiraData = new
        {
            VulnId = vulnId,
            Name = name,
            Reporter = reporter,
            Assignee = assignee,
            JiraType = jiraType,
            Label = label
        };

        return await _apiClient.PostAsync<object, Jira>($"api/Jira/{vulnId}", jiraData, cancellationToken);
    }

    [McpServerTool, Description("Update a JIRA issue for a vulnerability")]
    public async Task<bool> UpdateJiraIssueAsync(
        [Description("Vulnerability ID")] Guid vulnId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating JIRA issue for vulnerability: {VulnId}", vulnId);
        return await _apiClient.PostAsync($"api/Jira/UpdateIssue/{vulnId}", new { }, cancellationToken);
    }

    [McpServerTool, Description("Delete a JIRA issue for a vulnerability")]
    public async Task<bool> DeleteJiraIssueAsync(
        [Description("Vulnerability ID")] Guid vulnId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting JIRA issue for vulnerability: {VulnId}", vulnId);
        return await _apiClient.DeleteAsync($"api/Jira?vulnId={vulnId}", cancellationToken);
    }

    [McpServerTool, Description("Get JIRA comments for a vulnerability")]
    public async Task<List<JiraComments>> GetJiraCommentsAsync(
        [Description("Vulnerability ID")] Guid vulnId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching JIRA comments for vulnerability: {VulnId}", vulnId);
        var comments = await _apiClient.GetAsync<List<JiraComments>>($"api/Jira/Comments/{vulnId}", cancellationToken);
        return comments ?? new List<JiraComments>();
    }

    [McpServerTool, Description("Add a comment to a JIRA issue")]
    public async Task<JiraComments?> AddJiraCommentAsync(
        [Description("JIRA issue ID")] Guid jiraId,
        [Description("Comment body")] string body,
        [Description("JIRA comment ID")] string? jiraIdComment = null,
        [Description("Comment author")] string? author = null,
        [Description("Group level restriction")] string? groupLevel = null,
        [Description("Role level restriction")] string? roleLevel = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding comment to JIRA issue: {JiraId}", jiraId);
        
        var commentData = new JiraCreateCommentViewModel
        {
            JiraId = jiraId,
            JiraIdComment = jiraIdComment,
            Author = author,
            Body = body,
            GroupLevel = groupLevel,
            RoleLevel = roleLevel
        };

        return await _apiClient.PostAsync<JiraCreateCommentViewModel, JiraComments>("api/Jira/Comment", commentData, cancellationToken);
    }
}