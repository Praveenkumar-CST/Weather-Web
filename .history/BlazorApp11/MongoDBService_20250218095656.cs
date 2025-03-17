using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MongoDBService
{
    private readonly IMongoCollection<FavoriteCity> _favoriteCitiesCollection;

    public MongoDBService()
    {
        var client = new MongoClient("mongodb://localhost:27017");  // MongoDB connection string (ensure MongoDB is running)
        var database = client.GetDatabase("WeatherApp");           // Database name
        _favoriteCitiesCollection = database.GetCollection<FavoriteCity>("FavoriteCities"); // Collection name
    }

    public async Task<List<string>> GetFavoriteCitiesAsync()
    {
        try
        {
            var cities = await _favoriteCitiesCollection.Find(_ => true).ToListAsync();
            return cities.Select(c => c.CityName).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching favorite cities: {ex.Message}");
            return new List<string>(); // Return an empty list in case of error
        }
    }

    public async Task AddFavoriteCityAsync(string city)
    {
        var favoriteCity = new FavoriteCity { CityName = city };
        try
        {
            await _favoriteCitiesCollection.InsertOneAsync(favoriteCity);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding favorite city: {ex.Message}");
        }
    }

    public async Task RemoveFavoriteCityAsync(string city)
    {
        var filter = Builders<FavoriteCity>.Filter.Eq(c => c.CityName, city);
        try
        {
            await _favoriteCitiesCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error removing favorite city: {ex.Message}");
        }
    }
}

public class FavoriteCity
{
    public string CityName { get; set; }
}
