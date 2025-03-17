using MongoDB.Driver;

public class MongoDBService
{
    private readonly IMongoCollection<FavoriteCity> _favoriteCities;

    public MongoDBService(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _favoriteCities = database.GetCollection<FavoriteCity>("FavoriteCities");
    }

    public async Task<List<FavoriteCity>> GetFavoriteCitiesAsync()
    {
        return await _favoriteCities.Find(city => true).ToListAsync();
    }

    public async Task AddFavoriteCityAsync(FavoriteCity city)
    {
        await _favoriteCities.InsertOneAsync(city);
    }

    public async Task RemoveFavoriteCityAsync(string cityName)
    {
        await _favoriteCities.DeleteOneAsync(city => city.Name == cityName);
    }
}

public class FavoriteCity
{
    public string Name { get; set; }
    public DateTime AddedDate { get; set; }
}
