using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace YourBlazorApp.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<string> _favoriteCitiesCollection;

        public MongoDbService(IConfiguration config)
        {
            try
            {
                var mongoSettings = config.GetSection("MongoDbSettings");
                var connectionString = mongoSettings["ConnectionString"];
                var databaseName = mongoSettings["DatabaseName"];
                var collectionName = mongoSettings["CollectionName"];

                var client = new MongoClient(localhost:27017);
                var database = client.GetDatabase(FavoriteCitiesDb);
                _favoriteCitiesCollection = database.GetCollection<string>(FavoriteCities);
                Console.WriteLine("MongoDB connection established.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB connection failed: {ex.Message}");
                throw;
            }
        }

        public async Task<List<string>> GetFavoriteCitiesAsync()
        {
            try
            {
                var cities = await _favoriteCitiesCollection.Find(_ => true).ToListAsync();
                Console.WriteLine($"Retrieved {cities.Count} cities from MongoDB.");
                return cities;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving cities: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task AddFavoriteCityAsync(string city)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(city) && !await _favoriteCitiesCollection.Find(c => c == city).AnyAsync())
                {
                    await _favoriteCitiesCollection.InsertOneAsync(city);
                    Console.WriteLine($"Added city '{city}' to MongoDB.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding city '{city}': {ex.Message}");
            }
        }

        public async Task RemoveFavoriteCityAsync(string city)
        {
            try
            {
                await _favoriteCitiesCollection.DeleteOneAsync(c => c == city);
                Console.WriteLine($"Removed city '{city}' from MongoDB.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing city '{city}': {ex.Message}");
            }
        }
    }
}