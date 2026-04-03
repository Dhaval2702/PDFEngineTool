namespace PDFForge.Infrastructure.Options;

public sealed class TempFileOptions
{
    public const string SectionName = "TempFiles";

    public string RootPath { get; init; } = "wwwroot/temp";
    public int RetentionMinutes { get; init; } = 30;
    public int CleanupIntervalMinutes { get; init; } = 10;
}
