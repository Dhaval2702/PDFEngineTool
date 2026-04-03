using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PDFForge.Application.Abstractions;
using PDFForge.Application.Validation;

namespace PDFForge.Infrastructure.Services;

public sealed class FileValidator(IOptions<FileUploadValidationOptions> options) : IFileValidator
{
    private readonly FileUploadValidationOptions _options = options.Value;

    public IReadOnlyCollection<string> ValidateFiles(IEnumerable<IFormFile> files)
    {
        var errors = new List<string>();
        var fileList = files.ToList();

        if (fileList.Count == 0)
        {
            errors.Add("At least one file is required.");
            return errors;
        }

        if (fileList.Count > _options.MaxFilesPerUpload)
        {
            errors.Add($"You can upload up to {_options.MaxFilesPerUpload} files at once.");
        }

        foreach (var file in fileList)
        {
            if (file.Length <= 0)
            {
                errors.Add($"File '{file.FileName}' is empty.");
                continue;
            }

            if (file.Length > _options.MaxFileSizeInBytes)
            {
                errors.Add($"File '{file.FileName}' exceeds the maximum size of {_options.MaxFileSizeInBytes / (1024 * 1024)} MB.");
            }

            if (!FileValidationHelper.HasAllowedExtension(file.FileName, _options.AllowedExtensions))
            {
                errors.Add($"File '{file.FileName}' is not an allowed type.");
            }
        }

        return errors;
    }
}
