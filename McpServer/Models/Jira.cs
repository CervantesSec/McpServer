namespace McpServer.Models;

public class Jira
{
    public Guid Id { get; set; }
    public Guid VulnId { get; set; }
    public string? UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? JiraIdentifier { get; set; }
    public string? JiraKey { get; set; }
    public string? Name { get; set; }
    public string? Reporter { get; set; }
    public string? Assignee { get; set; }
    public string? JiraType { get; set; }
    public string? Label { get; set; }
    public long? Votes { get; set; }
}

public class JiraComments
{
    public Guid Id { get; set; }
    public Guid JiraId { get; set; }
    public string? JiraIdComment { get; set; }
    public string? Author { get; set; }
    public string? Body { get; set; }
    public string? GroupLevel { get; set; }
    public string? RoleLevel { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? UpdateAuthor { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public class JiraCreateCommentViewModel
{
    public Guid JiraId { get; set; }
    public string? JiraIdComment { get; set; }
    public string? Author { get; set; }
    public string? Body { get; set; }
    public string? GroupLevel { get; set; }
    public string? RoleLevel { get; set; }
}