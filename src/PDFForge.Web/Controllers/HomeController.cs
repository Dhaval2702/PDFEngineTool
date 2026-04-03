using Microsoft.AspNetCore.Mvc;
using PDFForge.Application.Abstractions;
using PDFForge.Web.Models;

namespace PDFForge.Web.Controllers;

public sealed class HomeController(IPdfToolCatalogService catalogService) : Controller
{
    public IActionResult Index()
    {
        var model = new HomeViewModel
        {
            ToolCards = catalogService.GetToolCards()
        };

        return View(model);
    }

    public IActionResult Error() => View();
}
