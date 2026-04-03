using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PDFForge.Infrastructure.Options;

namespace PDFForge.Infrastructure.BackgroundServices;

public sealed class FileCleanupService(
    IWebHostEnvironment environment,
    IOptions<TempFileOptions> options,
    ILogger<FileCleanupService> logger) : BackgroundService
{
    private readonly TempFileOptions _options = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                CleanupExpiredFiles(environment.WebRootPath);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error during temp file cleanup.");
            }

            await Task.Delay(TimeSpan.FromMinutes(_options.CleanupIntervalMinutes), stoppingToken);
        }
    }

    private void CleanupExpiredFiles(string webRootPath)
    {
        var baseFolder = Path.IsPathRooted(_options.RootPath)
            ? _options.RootPath
            : Path.Combine(webRootPath, _options.RootPath.Replace("wwwroot/", string.Empty, StringComparison.OrdinalIgnoreCase));

        if (!Directory.Exists(baseFolder))
        {
            return;
        }

        var threshold = DateTimeOffset.UtcNow.AddMinutes(-_options.RetentionMinutes);
        var deletedCount = 0;

        foreach (var file in Directory.EnumerateFiles(baseFolder))
        {
            var info = new FileInfo(file);
            if (info.LastWriteTimeUtc < threshold.UtcDateTime)
            {
                info.Delete();
                deletedCount++;
            }
        }

        if (deletedCount > 0)
        {
            logger.LogInformation("File cleanup removed {Count} expired files.", deletedCount);
        }
    }
}
