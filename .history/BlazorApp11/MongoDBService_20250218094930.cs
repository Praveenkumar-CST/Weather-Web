using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MongoDBService
{
    private readonly IMongoCollection<FavoriteCity> _favoriteCitiesCollection;

    public MongoDBService()
    {
        var client = new MongoClient("mongodb://localhost:27017");  // Your MongoDB connection string
        var database = client.GetDatabase("WeatherApp");           // Database name
        _favoriteCitiesCollection = database.GetCollection<FavoriteCity>("FavoriteCities"); // Collection name
    }

    public async Task<List<string>> GetFavoriteCitiesAsync()
    {
        var cities = await _favoriteCitiesCollection.Find(_ => true).ToListAsync();
        return cities.Select(c => c.CityName).ToList();
    }

    public async Task AddFavoriteCityAsync(string city)
    {
        var favoriteCity = new FavoriteCity { CityName = city };
        await _favoriteCitiesCollection.InsertOneAsync(favoriteCity);
    }

    public async Task RemoveFavoriteCityAsync(string city)
    {
        var filter = Builders<FavoriteCity>.Filter.Eq(c => c.CityName, city);
        await _favoriteCitiesCollection.DeleteOneAsync(filter);
    }
}

public class FavoriteCity
{
    public string CityName { get; set; }
}
