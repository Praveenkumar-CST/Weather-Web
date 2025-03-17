using MongoDB.Driver;

public class MongoService
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<FavoriteCity> _favoritesCollection;

    public MongoService()
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        _database = mongoClient.GetDatabase("WeatherApp");
        _favoritesCollection = _database.GetCollection<FavoriteCity>("Favorites");
    }

    public async Task AddCityToFavorites(string cityName)
    {
        var city = new FavoriteCity { CityName = cityName };
        await _favoritesCollection.InsertOneAsync(city);
    }

    public async Task<List<string>> GetFavoriteCities()
    {
        var cities = await _favoritesCollection.Find(_ => true).ToListAsync();
        return cities.Select(c => c.CityName).ToList();
    }
}

public class FavoriteCity
{
    public string CityName { get; set; }
}
