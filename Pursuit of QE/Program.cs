using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["InstrumentationKey=ed11009d-7470-4892-a65c-3f957c7b9f88;IngestionEndpoint=https://australiaeast-1.in.applicationinsights.azure.com/;LiveEndpoint=https://australiaeast.livediagnostics.monitor.azure.com/"]);

        builder.Services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(builder.Configuration["AccountEndpoint=https://pursuitofqeaccount.documents.azure.com:443/;AccountKey=BtUpdQNz37oGlgORo5QZgJ8GAk4zNbaq4XMge9ceOj8X5d1K58okWYcbqTyr0WanvIqjOoaUssJmACDbtloCgQ=="], preferMsi: true);
            clientBuilder.AddQueueServiceClient(builder.Configuration["AccountEndpoint=https://pursuitofqeaccount.documents.azure.com:443/;AccountKey=BtUpdQNz37oGlgORo5QZgJ8GAk4zNbaq4XMge9ceOj8X5d1K58okWYcbqTyr0WanvIqjOoaUssJmACDbtloCgQ=="], preferMsi: true);
        });

        var blobServiceClient = new BlobServiceClient(new Uri("https://pqestor.blob.core.windows.net/"), new DefaultAzureCredential());
        string containerName = "quickstartblobs" + Guid.NewGuid().ToString();
        BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

        string localPath = "data";
        Directory.CreateDirectory(localPath);
        string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
        string localFilePath = Path.Combine(localPath, fileName);

        await File.WriteAllTextAsync(localPath, "new document");
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
    }
}