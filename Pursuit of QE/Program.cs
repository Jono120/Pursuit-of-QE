using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Adding in read for the appsettings.json file
//var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddRazorPages();

//var connectionString = Configuration.GetConnectionString;
//string containerName = "photos";

//BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
//container.CreateIfNotExists();

builder.Services.AddRazorPages();

// Adding in live telemetry for Applicaiton Insights
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["InstrumentationKey=ed11009d-7470-4892-a65c-3f957c7b9f88;IngestionEndpoint=https://australiaeast-1.in.applicationinsights.azure.com/;LiveEndpoint=https://australiaeast.livediagnostics.monitor.azure.com/"]);

//builder.Services.AddAzureClients(clientBuilder =>
//{
//    clientBuilder.AddBlobServiceClient(builder.Configuration["appsettings.json"], preferMsi: true);
//    clientBuilder.AddQueueServiceClient(builder.Configuration["appsettings.json"], preferMsi: true);
//});

//var blobServiceClient = new BlobServiceClient(new Uri("https://pqestor.blob.core.windows.net/"), new DefaultAzureCredential());
//string containerName = "quickstartblobs" + Guid.NewGuid().ToString();
//BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

//string localPath = "data";
//Directory.CreateDirectory(localPath);
//string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
//string localFilePath = Path.Combine(localPath, fileName);

//await File.WriteAllTextAsync(localPath, "new document");
//BlobClient blobClient = containerClient.GetBlobClient(fileName);
//Console.WriteLine("Uploading to blob storage as blob:\n\t {0}\n", blobClient.Uri);



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
