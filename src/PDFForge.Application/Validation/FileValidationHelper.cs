namespace PDFForge.Application.Validation;

public static class FileValidationHelper
{
    public static bool HasAllowedExtension(string fileName, IReadOnlyCollection<string> allowedExtensions)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
    }
}
