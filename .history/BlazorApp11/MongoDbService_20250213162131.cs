using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MongoDbService
{
    private readonly IMongoCollection<City> _citiesCollection;

    public MongoDbService()
    {
        var client = new MongoClient("mongodb://localhost:27017"); // Connect to MongoDB
        var database = client.GetDatabase("WeatherApp"); // Use "WeatherApp" database
        _citiesCollection = database.GetCollection<City>("FavoriteCities"); // Collection name
    }

    public async Task<List<City>> GetFavoriteCities()
    {
        return await _citiesCollection.Find(_ => true).ToListAsync();
    }

    public async Task AddToFavorites(City city)
    {
        await _citiesCollection.InsertOneAsync(city);
    }

    public async Task RemoveFromFavorites(string cityName)
    {
        await _citiesCollection.DeleteOneAsync(c => c.Name == cityName);
    }
}

public class City
{
    public string Id { get; set; }
    public string Name { get; set; }
}
