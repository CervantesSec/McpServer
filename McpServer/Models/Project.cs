using System.Text.Json.Serialization;

namespace McpServer.Models;

public class Project
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }
    
    [JsonPropertyName("endDate")]
    public DateTime? EndDate { get; set; }
    
    [JsonPropertyName("template")]
    public bool Template { get; set; }
    
    [JsonPropertyName("status")]
    public ProjectStatus Status { get; set; }
    
    [JsonPropertyName("projectType")]
    public ProjectType ProjectType { get; set; }
    
    [JsonPropertyName("language")]
    public Language Language { get; set; }
    
    [JsonPropertyName("score")]
    public double Score { get; set; }
    
    [JsonPropertyName("findingsId")]
    public string? FindingsId { get; set; }
    
    [JsonPropertyName("executiveSummary")]
    public string? ExecutiveSummary { get; set; }
    
    [JsonPropertyName("clientId")]
    public Guid ClientId { get; set; }
    
    [JsonPropertyName("client")]
    public Client? Client { get; set; }
    
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }
    
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
}

public class ProjectCreateViewModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }
    
    [JsonPropertyName("endDate")]
    public DateTime? EndDate { get; set; }
    
    [JsonPropertyName("template")]
    public bool Template { get; set; }
    
    [JsonPropertyName("status")]
    public ProjectStatus Status { get; set; }
    
    [JsonPropertyName("projectType")]
    public ProjectType ProjectType { get; set; }
    
    [JsonPropertyName("language")]
    public Language Language { get; set; }
    
    [JsonPropertyName("clientId")]
    public Guid ClientId { get; set; }
    
    [JsonPropertyName("score")]
    public Score Score { get; set; }
    
    [JsonPropertyName("findingsId")]
    public string? FindingsId { get; set; }
    
    [JsonPropertyName("businessImpact")]
    public int BusinessImpact { get; set; }
}

public class ProjectEditViewModel : ProjectCreateViewModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("executiveSummary")]
    public string? ExecutiveSummary { get; set; }
}

public enum ProjectStatus
{
    Archived = 0,
    Active = 1,
    Waiting = 2
}

public enum ProjectType
{
    Internal = 0,
    External = 1
}

public enum Language
{
    English = 0,
    Español = 1
}

public enum Score
{
    Low = 0,
    High = 1
}