using PDFForge.Infrastructure.DependencyInjection;
using PDFForge.Infrastructure.Logging;
using PDFForge.Web.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Bootstraps structured logging early so startup/runtime failures are captured consistently.
LoggingSetup.ConfigureSerilog(builder.Configuration);
builder.Host.UseSerilog();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddPdfForgeServices(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
