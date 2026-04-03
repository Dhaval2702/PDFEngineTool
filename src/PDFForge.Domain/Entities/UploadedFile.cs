namespace PDFForge.Domain.Entities;

public sealed class UploadedFile
{
    public required string OriginalFileName { get; init; }
    public required string StoredFileName { get; init; }
    public required string FilePath { get; init; }
    public required string ContentType { get; init; }
    public long SizeInBytes { get; init; }
    public DateTimeOffset CreatedAtUtc { get; init; }
}
