using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WeatherBackend.Models; // Make sure this matches your actual folder structure

namespace WeatherBackend.Data
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _database;

        public AppDbContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<UserFavorites> UserFavorites =>
            _database.GetCollection<UserFavorites>("UserFavorites");
    }
}
