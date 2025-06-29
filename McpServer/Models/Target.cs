using System.Text.Json.Serialization;

namespace McpServer.Models;

public class Target
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("type")]
    public TargetType Type { get; set; }
    
    [JsonPropertyName("projectId")]
    public Guid ProjectId { get; set; }
    
    [JsonPropertyName("project")]
    public Project? Project { get; set; }
    
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }
    
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
}

public class TargetCreateViewModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("type")]
    public TargetType Type { get; set; }
    
    [JsonPropertyName("projectId")]
    public Guid ProjectId { get; set; }
}

public class TargetServices
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("port")]
    public int Port { get; set; }
    
    [JsonPropertyName("version")]
    public string? Version { get; set; }
    
    [JsonPropertyName("note")]
    public string? Note { get; set; }
    
    [JsonPropertyName("targetId")]
    public Guid TargetId { get; set; }
    
    [JsonPropertyName("target")]
    public Target? Target { get; set; }
    
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }
    
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
}

public enum TargetType
{
    Domain = 0,
    Ip = 1,
    Binary = 2,
    CIDR = 3,
    Hostname = 4
}