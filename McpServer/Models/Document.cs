namespace McpServer.Models;

public enum Visibility
{
    Private = 0,
    Public = 1
}

public class Document
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? FilePath { get; set; }
    public string? UserId { get; set; }
    public Visibility Visibility { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class DocumentCreateViewModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? FileName { get; set; }
    public byte[]? FileContent { get; set; }
}

public class DocumentEditViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}