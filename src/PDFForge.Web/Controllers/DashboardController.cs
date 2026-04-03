using Microsoft.AspNetCore.Mvc;
using PDFForge.Application.Abstractions;
using PDFForge.Web.Models;

namespace PDFForge.Web.Controllers;

[Route("dashboard")]
public sealed class DashboardController(IFileValidator fileValidator, ITempFileService tempFileService) : Controller
{
    [HttpGet("")]
    public IActionResult Index() => View();

    [HttpGet("{tool}")]
    public IActionResult Tool(string tool)
    {
        ViewData["ToolName"] = tool;
        return View("Tool");
    }

    [HttpPost("upload")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadFiles(FileUploadRequest request, CancellationToken cancellationToken)
    {
        var filesToProcess = request.Files ?? [];
        var errors = fileValidator.ValidateFiles(filesToProcess);
        if (errors.Count > 0)
        {
            return BadRequest(new { errors });
        }

        var files = await tempFileService.SaveFilesAsync(filesToProcess, cancellationToken);
        return Ok(new { files = files.Select(f => new { f.OriginalFileName, f.StoredFileName, f.SizeInBytes }) });
    }
}
