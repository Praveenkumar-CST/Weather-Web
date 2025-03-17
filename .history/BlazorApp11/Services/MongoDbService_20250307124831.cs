using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MongoDbService
{
    private readonly IMongoCollection<FavoriteCity> _favoriteCitiesCollection;

    public MongoDbService()
    {
        var client = new MongoClient("mongodb://localhost:27017"); // Adjust connection string if needed
        var database = client.GetDatabase("FavoriteCitiesDb");
        _favoriteCitiesCollection = database.GetCollection<FavoriteCity>("FavoriteCities");
    }

    public async Task<List<string>> GetFavoriteCitiesAsync()
    {
        var cities = await _favoriteCitiesCollection.Find(_ => true).ToListAsync();
        return cities.ConvertAll(c => c.Name); // Convert to list of city names
    }

    public async Task AddFavoriteCityAsync(string city)
    {
        var existingCity = await _favoriteCitiesCollection.Find(c => c.Name == city).FirstOrDefaultAsync();
        if (existingCity == null)
        {
            await _favoriteCitiesCollection.InsertOneAsync(new FavoriteCity { Name = city });
        }
    }

    public async Task RemoveFavoriteCityAsync(string city)
    {
        await _favoriteCitiesCollection.DeleteOneAsync(c => c.Name == city);
    }
}

public class FavoriteCity
{
    public string Id { get; set; } // MongoDB _id
    public string Name { get; set; }
}
