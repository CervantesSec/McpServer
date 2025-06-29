using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class LogTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<LogTool> _logger;

    public LogTool(CervantesApiClient apiClient, ILogger<LogTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get all system logs")]
    public async Task<List<Log>> GetLogsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all system logs");
        var logs = await _apiClient.GetAsync<List<Log>>("api/Log", cancellationToken);
        return logs ?? new List<Log>();
    }

    /*[McpServerTool, Description("Create a new log entry")]
    public async Task<bool> CreateLogEntryAsync(
        [Description("Log level (e.g., Info, Warning, Error, Debug)")] string level,
        [Description("Log message")] string message,
        [Description("Logger name")] string? logger = null,
        [Description("Exception details")] string? exception = null,
        [Description("Stack trace")] string? stackTrace = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new log entry with level: {LogLevel}", level);
        
        var logData = new
        {
            Level = level,
            Message = message,
            Logger = logger,
            Exception = exception,
            StackTrace = stackTrace,
            CreatedOn = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
        };

        return await _apiClient.PostAsync("api/Log", logData, cancellationToken);
    }*/
}