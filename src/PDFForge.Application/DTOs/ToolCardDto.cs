using PDFForge.Domain.Enums;

namespace PDFForge.Application.DTOs;

public sealed record ToolCardDto(
    PdfToolType ToolType,
    string Title,
    string Description,
    string IconClass,
    string Route);
