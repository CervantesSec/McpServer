namespace McpServer.Models;

public class Note
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? UserId { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class NoteCreateViewModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class NoteEditViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}