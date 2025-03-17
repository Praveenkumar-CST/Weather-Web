using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp11.Services // Matches your project namespace from Program.cs
{
    public class MongoDbService
    {
        private readonly IMongoCollection<string> _favoriteCitiesCollection;

        /// <summary>
        /// Initializes the MongoDB connection with hardcoded settings.
        /// </summary>
        public MongoDbService()
        {
            try
            {
                // Hardcoded MongoDB settings
                var connectionString = "mongodb://localhost:27017"; // Default local MongoDB connection
                var databaseName = "FavoriteCitiesDb"; // Name of the database
                var collectionName = "FavoriteCities"; // Name of the collection

                // Initialize MongoDB client, database, and collection
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);
                _favoriteCitiesCollection = database.GetCollection<string>(collectionName);

                // Confirm successful connection
                Console.WriteLine("MongoDB connection established.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB connection failed: {ex.Message}");
                throw; // Rethrow to halt service initialization if connection fails
            }
        }

        /// <summary>
        /// Retrieves all favorite cities from the MongoDB collection.
        /// </summary>
        /// <returns>A list of city names stored in the collection.</returns>
        public async Task<List<string>> GetFavoriteCitiesAsync()
        {
            try
            {
                // Fetch all cities from the collection
                var cities = await _favoriteCitiesCollection.Find(_ => true).ToListAsync();
                Console.WriteLine($"Retrieved {cities.Count} cities from MongoDB.");
                return cities;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving cities: {ex.Message}");
                return new List<string>(); // Return empty list on failure
            }
        }

        /// <summary>
        /// Adds a city to the favorite cities collection if it doesn’t already exist.
        /// </summary>
        /// <param name="city">The name of the city to add.</param>
        public async Task AddFavoriteCityAsync(string city)
        {
            try
            {
                // Validate input and check for duplicates
                if (!string.IsNullOrWhiteSpace(city) && !await _favoriteCitiesCollection.Find(c => c == city).AnyAsync())
                {
                    await _favoriteCitiesCollection.InsertOneAsync(city);
                    Console.WriteLine($"Added city '{city}' to MongoDB.");
                }
                else if (string.IsNullOrWhiteSpace(city))
                {
                    Console.WriteLine("City name cannot be empty.");
                }
                else
                {
                    Console.WriteLine($"City '{city}' already exists in favorites.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding city '{city}': {ex.Message}");
            }
        }

        /// <summary>
        /// Removes a city from the favorite cities collection.
        /// </summary>
        /// <param name="city">The name of the city to remove.</param>
        public async Task RemoveFavoriteCityAsync(string city)
        {
            try
            {
                // Remove the city if it exists
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