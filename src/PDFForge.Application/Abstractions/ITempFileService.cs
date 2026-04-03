using Microsoft.AspNetCore.Http;
using PDFForge.Domain.Entities;

namespace PDFForge.Application.Abstractions;

public interface ITempFileService
{
    Task<IReadOnlyCollection<UploadedFile>> SaveFilesAsync(IEnumerable<IFormFile> files, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string path, CancellationToken cancellationToken = default);
}
