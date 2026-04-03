using PDFForge.Application.Abstractions;
using PDFForge.Application.DTOs;
using PDFForge.Domain.Enums;

namespace PDFForge.Application.Services;

public sealed class PdfToolCatalogService : IPdfToolCatalogService
{
    public IReadOnlyCollection<ToolCardDto> GetToolCards() =>
    [
        new(PdfToolType.MergePdf, "Merge PDF", "Combine multiple PDFs into one single document.", "bi bi-file-earmark-plus", "/dashboard/merge"),
        new(PdfToolType.SplitPdf, "Split PDF", "Extract selected pages into smaller files.", "bi bi-scissors", "/dashboard/split"),
        new(PdfToolType.CompressPdf, "Compress PDF", "Reduce file size while preserving quality.", "bi bi-archive", "/dashboard/compress"),
        new(PdfToolType.ImageToPdf, "Image to PDF", "Convert JPG and PNG images into polished PDFs.", "bi bi-image", "/dashboard/image-to-pdf"),
        new(PdfToolType.ProtectPdf, "Protect PDF", "Encrypt and password-protect sensitive documents.", "bi bi-lock", "/dashboard/protect")
    ];
}
