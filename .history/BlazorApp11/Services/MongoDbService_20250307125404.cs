using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp11.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<FavoriteCity> _favoriteCitiesCollection;

        public MongoDbService()
        {
            var client = new MongoClient("mongodb://localhost:27017"); // Change to your MongoDB connection if needed
            var database = client.GetDatabase("FavoriteCitiesDb"); // Replace with your database name
            _favoriteCitiesCollection = database.GetCollection<FavoriteCity>("FavoriteCities");
        }

        public async Task<List<string>> GetFavoriteCitiesAsync()
        {
            var cities = await _favoriteCitiesCollection.Find(_ => true).ToListAsync();
            return cities.ConvertAll(c => c.Name);
        }

        public async Task AddFavoriteCityAsync(string city)
        {
            var existingCity = await _favoriteCitiesCollection.Find(c => c.Name == city).FirstOrDefaultAsync();
            if (existingCity == null)
            {
                await _favoriteCitiesCollection.InsertOneAsync(new FavoriteCity { Name = city });
            }
        }

        public async Task RemoveFavoriteCityAsync(string city)
        {
            await _favoriteCitiesCollection.DeleteOneAsync(c => c.Name == city);
        }
    }

    public class FavoriteCity
    {
        public ObjectId Id { get; set; } // MongoDB _id
        public string Name { get; set; }
    }
}
