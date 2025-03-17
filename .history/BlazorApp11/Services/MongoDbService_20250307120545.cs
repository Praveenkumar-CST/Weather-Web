using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourBlazorApp.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<string> _favoriteCitiesCollection;

        public MongoDbService()
        {
            // Connection string for MongoDB running on localhost:27017
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("FavoriteCitiesDb"); // Database name
            _favoriteCitiesCollection = database.GetCollection<string>("FavoriteCities"); // Collection name
        }

        // Get all favorite cities
        public async Task<List<string>> GetFavoriteCitiesAsync()
        {
            return await _favoriteCitiesCollection.Find(_ => true).ToListAsync();
        }

        // Add a city to favorites
        public async Task AddFavoriteCityAsync(string city)
        {
            // Check if the city already exists to avoid duplicates
            if (!await _favoriteCitiesCollection.Find(c => c == city).AnyAsync())
            {
                await _favoriteCitiesCollection.InsertOneAsync(city);
            }
        }

        // Remove a city from favorites
        public async Task RemoveFavoriteCityAsync(string city)
        {
            await _favoriteCitiesCollection.DeleteOneAsync(c => c == city);
        }
    }
}