namespace PDFForge.Application.Validation;

public sealed class FileUploadValidationOptions
{
    public const string SectionName = "FileUpload";

    public long MaxFileSizeInBytes { get; init; } = 20 * 1024 * 1024;
    public int MaxFilesPerUpload { get; init; } = 10;
    public List<string> AllowedExtensions { get; init; } = [".pdf", ".jpg", ".png"];
}
