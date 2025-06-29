using McpServer.Models;
using McpServer.Services;
using McpServer.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using Task = System.Threading.Tasks.Task;

namespace McpServer;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        // Configuration
        // Search for appsettings.json in development and production locations
        var baseDirectory = AppContext.BaseDirectory;
        var configFile = "appsettings.json";
        
        // Check if we're running from bin/Debug or bin/Release (development)
        string configPath;
        if (baseDirectory.Contains("bin"))
        {
            // Go up 3 levels from bin/Debug/net9.0 to project root
            var projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, "..", "..", ".."));
            configPath = Path.Combine(projectRoot, configFile);
        }
        else
        {
            // Production: config file should be in the same directory as the executable
            configPath = Path.Combine(baseDirectory, configFile);
        }

        if (!File.Exists(configPath))
        {
            throw new FileNotFoundException($"Configuration file '{configFile}' not found at: {configPath}");
        }

        builder.Configuration
            .SetBasePath(Path.GetDirectoryName(configPath)!)
            .AddJsonFile(Path.GetFileName(configPath), optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args);

        builder.Services.Configure<CervantesConfiguration>(
            builder.Configuration.GetSection(CervantesConfiguration.SectionName));

        // Disable console logging for MCP
        builder.Logging.ClearProviders();
        
        // HTTP Client and Services
        builder.Services.AddHttpClient<CervantesApiClient>();
        builder.Services.AddSingleton<CervantesApiClient>();

        // Tools
        builder.Services.AddSingleton<ClientTool>();
        builder.Services.AddSingleton<ProjectTool>();
        builder.Services.AddSingleton<VulnerabilityTool>();
        builder.Services.AddSingleton<TaskTool>();
        builder.Services.AddSingleton<TargetTool>();
        builder.Services.AddSingleton<ReportTool>();
        builder.Services.AddSingleton<DocumentTool>();
        builder.Services.AddSingleton<VaultTool>();
        builder.Services.AddSingleton<NoteTool>();
        builder.Services.AddSingleton<UserTool>();
        builder.Services.AddSingleton<RoleTool>();
        builder.Services.AddSingleton<KnowledgeBaseTool>();
        builder.Services.AddSingleton<JiraTool>();
        builder.Services.AddSingleton<LogTool>();

        // MCP Server
        builder.Services
            .AddMcpServer()
            .WithStdioServerTransport()
            .WithToolsFromAssembly();

        var host = builder.Build();
        
        await host.RunAsync();
    }
}