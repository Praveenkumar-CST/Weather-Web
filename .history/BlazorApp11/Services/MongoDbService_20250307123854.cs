using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp11.Services // Must match the using directive in Razor files
{
    public class MongoDbService
    {
        private readonly IMongoCollection<string> _favoriteCitiesCollection;

        public MongoDbService()
        {
            try
            {
                var connectionString = "mongodb://localhost:27017";
                var databaseName = "FavoriteCitiesDb";
                var collectionName = "FavoriteCities";

                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);
                _favoriteCitiesCollection = database.GetCollection<string>(collectionName);
                Console.WriteLine("MongoDB connection established.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB connection failed: {ex.Message}");
                throw;
            }
        }

        public async Task<List<string>> GetFavoriteCitiesAsync() { /* ... */ }
        public async Task AddFavoriteCityAsync(string city) { /* ... */ }
        public async Task RemoveFavoriteCityAsync(string city) { /* ... */ }
    }
}