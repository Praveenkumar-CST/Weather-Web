using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp11.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<FavoriteCity> _favoriteCitiesCollection;

        public MongoDbService(IConfigurationSection mongoDbSettings)
        {
            var connectionString = mongoDbSettings["ConnectionString"];
            var databaseName = mongoDbSettings["DatabaseName"];
            var collectionName = mongoDbSettings["CollectionName"];

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _favoriteCitiesCollection = database.GetCollection<FavoriteCity>(collectionName);
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
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
