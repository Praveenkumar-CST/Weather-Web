using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDbService
{
    private readonly IMongoCollection<BsonDocument> _citiesCollection;

    public MongoDbService()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("WeatherApp");
        _citiesCollection = database.GetCollection<BsonDocument>("FavoriteCities");
    }

    public async Task<List<string>> GetFavoriteCities()
    {
        var cities = await _citiesCollection.Find(new BsonDocument()).ToListAsync();
        return cities.Select(city => city["name"].AsString).ToList();
    }

    public async Task AddFavoriteCity(string cityName)
    {
        var cityDocument = new BsonDocument { { "name", cityName } };
        await _citiesCollection.InsertOneAsync(cityDocument);
    }

    public async Task RemoveFavoriteCity(string cityName)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("name", cityName);
        await _citiesCollection.DeleteOneAsync(filter);
    }
}
