using PDFForge.Application.DTOs;

namespace PDFForge.Web.Models;

public sealed class HomeViewModel
{
    public required IReadOnlyCollection<ToolCardDto> ToolCards { get; init; }
}
