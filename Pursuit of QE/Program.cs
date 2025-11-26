using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Access the configuration from the builder
var configuration = builder.Configuration;
// Replace "DefaultConnection" with your actual connection string name in appsettings.json
var connectionString = configuration.GetConnectionString("StorageAccount");
string containerName = "photos";

BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
container.CreateIfNotExists();

// Adding in live telemetry for Application Insights
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = configuration["ApplicationInsights:ConnectionString"];
});

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(configuration["appsettings.json"], preferMsi: true);
    clientBuilder.AddQueueServiceClient(configuration["appsettings.json"], preferMsi: true);
});

var blobServiceClient = new BlobServiceClient(new Uri("https://pqestor.blob.core.windows.net/"), new DefaultAzureCredential());
string containerName2 = "quickstartblobs" + Guid.NewGuid().ToString();
BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName2);

string localPath = "data";
Directory.CreateDirectory(localPath);
string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
string localFilePath = Path.Combine(localPath, fileName);

await File.WriteAllTextAsync(localFilePath, "new document");
BlobClient blobClient = containerClient.GetBlobClient(fileName);
Console.WriteLine("Uploading to blob storage as blob:\n\t {0}\n", blobClient.Uri);



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
