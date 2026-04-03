using PDFForge.Application.DTOs;

namespace PDFForge.Application.Abstractions;

public interface IPdfToolCatalogService
{
    IReadOnlyCollection<ToolCardDto> GetToolCards();
}
