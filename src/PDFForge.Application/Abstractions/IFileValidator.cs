using Microsoft.AspNetCore.Http;

namespace PDFForge.Application.Abstractions;

public interface IFileValidator
{
    IReadOnlyCollection<string> ValidateFiles(IEnumerable<IFormFile> files);
}
