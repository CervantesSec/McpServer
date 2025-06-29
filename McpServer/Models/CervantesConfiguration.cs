namespace McpServer.Models;

public class CervantesConfiguration
{
    public const string SectionName = "Cervantes";
    
    public string BaseUrl { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string AuthMethod { get; set; } = "BasicAuth";
}