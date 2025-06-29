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
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args);

        builder.Services.Configure<CervantesConfiguration>(
            builder.Configuration.GetSection(CervantesConfiguration.SectionName));

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
        
        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Starting Cervantes MCP Server...");
        
        try
        {
            await host.RunAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while running the MCP server");
            throw;
        }
    }
}