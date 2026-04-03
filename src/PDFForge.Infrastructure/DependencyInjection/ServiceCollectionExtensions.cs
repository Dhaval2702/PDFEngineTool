using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PDFForge.Application.Abstractions;
using PDFForge.Application.Services;
using PDFForge.Application.Validation;
using PDFForge.Infrastructure.BackgroundServices;
using PDFForge.Infrastructure.Options;
using PDFForge.Infrastructure.Services;

namespace PDFForge.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPdfForgeServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileUploadValidationOptions>(configuration.GetSection(FileUploadValidationOptions.SectionName));
        services.Configure<TempFileOptions>(configuration.GetSection(TempFileOptions.SectionName));

        services.AddScoped<IFileValidator, FileValidator>();
        services.AddScoped<ITempFileService, TempFileService>();
        services.AddSingleton<IPdfToolCatalogService, PdfToolCatalogService>();

        services.AddHostedService<FileCleanupService>();

        return services;
    }
}
