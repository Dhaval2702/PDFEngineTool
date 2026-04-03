using Microsoft.AspNetCore.Http;

namespace PDFForge.Web.Models;

public sealed class FileUploadRequest
{
    public List<IFormFile>? Files { get; init; }
}
