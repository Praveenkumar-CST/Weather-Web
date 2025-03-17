using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;

public class MongoDbService
{
    private readonly IMongoCollection<NameEntry> _collection;

    public MongoDbService()
    {
        try
        {
            var settings = new MongoDbSettings();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<NameEntry>(settings.CollectionName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MongoDB Connection Error: {ex.Message}");
        }
    }

    public async Task InsertNameAsync(string name)
    {
        try
        {
            var entry = new NameEntry { Id = ObjectId.GenerateNewId().ToString(), Name = name };
            await _collection.InsertOneAsync(entry);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MongoDB Insert Error: {ex.Message}");
            throw;
        }
    }
}

// MongoDB Model
public class NameEntry
{
    public string Id { get; set; }
    public string Name { get; set; }
}

// MongoDB Settings
public class MongoDbSettings
{
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    public string DatabaseName { get; set; } = "MyDatabase";
    public string CollectionName { get; set; } = "History";
}
