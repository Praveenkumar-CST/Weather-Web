using MongoDB.Driver;
using WeatherBackend.Entity;
using System.Collections.Generic;
using WeatherBackend;

namespace WeatherBackend.Data
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _database;

        public AppDbContext(IConfiguration configuration)
        {
            var connectionStrings = configuration.GetConnectionString("MongoDb"); // name, not the URL directly
            var databaseName = configuration.GetConnectionString("Database");


            var Client = new MongoClient(connectionStrings);
            _database = Client.GetDatabase(databaseName);
        }
        public IMongoCollection<UserFavorites> FavoriteCity =>
            _database.GetCollection<UserFavorites>("FavoriteCity");

    }

}
