using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

/*
 * https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=visual-studio%2Cmanaged-identity%2Croles-azure-portal%2Csign-in-azure-cli%2Cidentity-visual-studio
 * https://learn.microsoft.com/en-us/visualstudio/azure/vs-azure-tools-connected-services-storage?view=vs-2022
 * https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
 * https://learn.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-dotnet-sqldatabase
 * https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0&tabs=visual-studio
 * https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-7.0
 * https://learn.microsoft.com/en-us/aspnet/web-pages/overview/getting-started/introducing-aspnet-web-pages-2/entering-data
 * https://learn.microsoft.com/en-us/troubleshoot/developer/webapps/aspnet/development/upload-file-to-web-site
 * https://learn.microsoft.com/en-us/aspnet/web-pages/overview/ui-layouts-and-themes/validating-user-input-in-aspnet-web-pages-sites
 * https://learn.microsoft.com/en-us/aspnet/web-pages/overview/ui-layouts-and-themes/3-creating-a-consistent-look
 * 
*/