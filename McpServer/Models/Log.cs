namespace McpServer.Models;

public class Log
{
    public int Id { get; set; }
    public string? CreatedOn { get; set; }
    public string? Level { get; set; }
    public string? Message { get; set; }
    public string? StackTrace { get; set; }
    public string? Exception { get; set; }
    public string? Logger { get; set; }
}