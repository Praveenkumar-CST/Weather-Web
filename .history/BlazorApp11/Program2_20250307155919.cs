using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace BlazorApp11 // Change this to match your project namespace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register MongoDB Service
            builder.Services.AddSingleton<MongoDbService>();

            // Add Razor Pages and Blazor
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }

    // MongoDB Settings Class
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string DatabaseName { get; set; } = "MyDatabase";
        public string CollectionName { get; set; } = "History";
    }

    // Name Entry Model
    public class NameEntry
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    // MongoDB Service
    public class MongoDbService
    {
        private readonly IMongoCollection<NameEntry> _collection;

        public MongoDbService()
        {
            var settings = new MongoDbSettings();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<NameEntry>(settings.CollectionName);
        }

        public async Task InsertNameAsync(string name)
        {
            var entry = new NameEntry { Id = ObjectId.GenerateNewId().ToString(), Name = name };
            await _collection.InsertOneAsync(entry);
        }
    }
}