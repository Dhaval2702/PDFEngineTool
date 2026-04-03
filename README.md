# PDFForge - Phase 1 Foundation

This repository contains a production-oriented Clean Architecture starter for an ASP.NET Core 8 PDF tool.

## Solution structure

- `src/PDFForge.Web`: MVC UI, middleware, controllers, Razor views, static assets.
- `src/PDFForge.Application`: use-case contracts, validation abstractions, tool catalog.
- `src/PDFForge.Domain`: core entities and enums with no infrastructure dependencies.
- `src/PDFForge.Infrastructure`: technical implementations (temp storage, cleanup service, logging, DI wiring).

## NuGet packages used

- `Serilog.AspNetCore`: structured logging and ASP.NET host integration.
- `Serilog.Sinks.Console`: local/dev diagnostics.
- `Serilog.Sinks.File`: rolling operational logs.
- `Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation`: improves MVC development flow.
- `Microsoft.Extensions.Options`: strongly-typed options binding support.
- `Microsoft.Extensions.Hosting.Abstractions`: background service primitives.

## Why key pieces exist

- `IFileValidator`: centralizes validation rules so UI/API controllers stay thin.
- `ITempFileService`: abstracts temp storage strategy (disk now, cloud later).
- `FileCleanupService`: enforces retention and avoids temp disk bloat.
- `GlobalExceptionHandlingMiddleware`: consistent error payloads and centralized logging.
- `PdfToolCatalogService`: gives a single source for tool cards/routes shown in UI.
