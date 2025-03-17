using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace BlazorApp11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<MongoDbService>();

            var serviceProvider = services.BuildServiceProvider();
            var mongoService = serviceProvider.GetRequiredService<MongoDbService>();

            // Application logic can be added here
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