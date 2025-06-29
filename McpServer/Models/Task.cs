using System.Text.Json.Serialization;

namespace McpServer.Models;

public class Task
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
    
    [JsonPropertyName("status")]
    public TaskStatus Status { get; set; }
    
    [JsonPropertyName("template")]
    public bool Template { get; set; }
    
    [JsonPropertyName("asignedUserId")]
    public string? AsignedUserId { get; set; }
    
    [JsonPropertyName("projectId")]
    public Guid? ProjectId { get; set; }
    
    [JsonPropertyName("project")]
    public Project? Project { get; set; }
    
    [JsonPropertyName("clientId")]
    public Guid? ClientId { get; set; }
    
    [JsonPropertyName("client")]
    public Client? Client { get; set; }
    
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }
    
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
}

public class TaskCreateViewModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }
    
    [JsonPropertyName("endDate")]
    public DateTime? EndDate { get; set; }
    
    [JsonPropertyName("status")]
    public TaskStatus Status { get; set; }
    
    [JsonPropertyName("template")]
    public bool Template { get; set; }
    
    [JsonPropertyName("asignedUserId")]
    public string? AsignedUserId { get; set; }
    
    [JsonPropertyName("projectId")]
    public Guid? ProjectId { get; set; }
    
    [JsonPropertyName("clientId")]
    public Guid? ClientId { get; set; }
}

public class TaskUpdateViewModel
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
    
    [JsonPropertyName("status")]
    public TaskStatus Status { get; set; }
    
    [JsonPropertyName("template")]
    public bool Template { get; set; }
    
    [JsonPropertyName("asignedUserId")]
    public string? AsignedUserId { get; set; }
    
    [JsonPropertyName("projectId")]
    public Guid? ProjectId { get; set; }
    
    [JsonPropertyName("clientId")]
    public Guid? ClientId { get; set; }
}

public class TaskNoteViewModel
{
    [JsonPropertyName("taskId")]
    public Guid TaskId { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("visibility")]
    public Visibility Visibility { get; set; }
}

public class TaskTargetViewModel
{
    [JsonPropertyName("taskId")]
    public Guid TaskId { get; set; }
    
    [JsonPropertyName("targetId")]
    public Guid TargetId { get; set; }
}

public class TaskAttachmentViewModel
{
    [JsonPropertyName("taskId")]
    public Guid TaskId { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("fileName")]
    public string? FileName { get; set; }
    
    [JsonPropertyName("fileContent")]
    public byte[]? FileContent { get; set; }
}

public enum TaskStatus
{
    Waiting = 0,
    InProgress = 1,
    Blocked = 2,
    Ready = 3,
    Completed = 4
}