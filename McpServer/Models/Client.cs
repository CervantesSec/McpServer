using System.Text.Json.Serialization;

namespace McpServer.Models;

public class Client
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    
    [JsonPropertyName("contactName")]
    public string? ContactName { get; set; }
    
    [JsonPropertyName("contactEmail")]
    public string? ContactEmail { get; set; }
    
    [JsonPropertyName("contactPhone")]
    public string? ContactPhone { get; set; }
    
    [JsonPropertyName("imagePath")]
    public string? ImagePath { get; set; }
    
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }
}

public class ClientCreateViewModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    
    [JsonPropertyName("contactName")]
    public string? ContactName { get; set; }
    
    [JsonPropertyName("contactEmail")]
    public string? ContactEmail { get; set; }
    
    [JsonPropertyName("contactPhone")]
    public string? ContactPhone { get; set; }
    
    [JsonPropertyName("fileName")]
    public string? FileName { get; set; }
    
    [JsonPropertyName("fileContent")]
    public byte[]? FileContent { get; set; }
}

public class ClientEditViewModel : ClientCreateViewModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}