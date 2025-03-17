using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp11.Services;
namespace BlazorApp11.Services
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

                Console.WriteLine("Attempting to connect to MongoDB...");
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);
                _favoriteCitiesCollection = database.GetCollection<string>(collectionName);

                // Test the connection by listing collections (optional, for debugging)
                var collections = database.ListCollectionNames().ToList();
                Console.WriteLine($"Connected to MongoDB. Found {collections.Count} collections in {databaseName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB connection failed: {ex.Message}");
                throw; // This ensures the app fails if MongoDB isn’t accessible
            }
        }

        public async Task<List<string>> GetFavoriteCitiesAsync()
        {
            try
            {
                Console.WriteLine("Fetching favorite cities...");
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
                Console.WriteLine($"Attempting to add city: '{city}'");
                if (string.IsNullOrWhiteSpace(city))
                {
                    Console.WriteLine("City name is empty or null. Skipping addition.");
                    return;
                }

                // Check if the city already exists
                var exists = await _favoriteCitiesCollection.Find(c => c == city).AnyAsync();
                if (exists)
                {
                    Console.WriteLine($"City '{city}' already exists in MongoDB. Skipping addition.");
                    return;
                }

                // Add the city
                await _favoriteCitiesCollection.InsertOneAsync(city);
                Console.WriteLine($"Successfully added city '{city}' to MongoDB.");
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
                Console.WriteLine($"Attempting to remove city: '{city}'");
                var result = await _favoriteCitiesCollection.DeleteOneAsync(c => c == city);
                if (result.DeletedCount > 0)
                {
                    Console.WriteLine($"Removed city '{city}' from MongoDB.");
                }
                else
                {
                    Console.WriteLine($"City '{city}' not found in MongoDB.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing city '{city}': {ex.Message}");
            }
        }
    }
}