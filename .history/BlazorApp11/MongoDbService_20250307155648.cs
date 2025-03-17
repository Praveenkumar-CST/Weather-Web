using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MongoDbService
{
    private readonly IMongoCollection<NameEntry> _collection;

    public MongoDbService(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<NameEntry>(settings.Value.CollectionName);
    }

    public async Task InsertNameAsync(string name)
    {
        var entry = new NameEntry { Id = ObjectId.GenerateNewId().ToString(), Name = name };
        await _collection.InsertOneAsync(entry);
    }
}

public class NameEntry
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}
