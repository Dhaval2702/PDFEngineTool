using Microsoft.Extensions.Configuration;
using Serilog;

namespace PDFForge.Infrastructure.Logging;

public static class LoggingSetup
{
    public static void ConfigureSerilog(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/pdf-forge-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}
