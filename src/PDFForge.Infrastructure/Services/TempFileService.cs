using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PDFForge.Application.Abstractions;
using PDFForge.Domain.Entities;
using PDFForge.Infrastructure.Options;

namespace PDFForge.Infrastructure.Services;

public sealed class TempFileService(
    IWebHostEnvironment environment,
    IOptions<TempFileOptions> options,
    ILogger<TempFileService> logger) : ITempFileService
{
    private readonly TempFileOptions _options = options.Value;

    public async Task<IReadOnlyCollection<UploadedFile>> SaveFilesAsync(IEnumerable<IFormFile> files, CancellationToken cancellationToken = default)
    {
        var tempDirectory = ResolveTempDirectory(environment.WebRootPath);
        Directory.CreateDirectory(tempDirectory);

        var savedFiles = new List<UploadedFile>();

        foreach (var file in files)
        {
            var safeFileName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName).ToLowerInvariant()}";
            var destinationPath = Path.Combine(tempDirectory, safeFileName);

            await using var stream = File.Create(destinationPath);
            await file.CopyToAsync(stream, cancellationToken);

            savedFiles.Add(new UploadedFile
            {
                OriginalFileName = file.FileName,
                StoredFileName = safeFileName,
                FilePath = destinationPath,
                ContentType = file.ContentType,
                SizeInBytes = file.Length,
                CreatedAtUtc = DateTimeOffset.UtcNow
            });
        }

        logger.LogInformation("Saved {Count} files in temp storage.", savedFiles.Count);
        return savedFiles;
    }

    public Task DeleteFileAsync(string path, CancellationToken cancellationToken = default)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            logger.LogInformation("Deleted temp file at {Path}", path);
        }

        return Task.CompletedTask;
    }

    private string ResolveTempDirectory(string webRootPath)
    {
        if (Path.IsPathRooted(_options.RootPath))
        {
            return _options.RootPath;
        }

        return Path.Combine(webRootPath, _options.RootPath.Replace("wwwroot/", string.Empty, StringComparison.OrdinalIgnoreCase));
    }
}
