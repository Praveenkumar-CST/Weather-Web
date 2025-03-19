using MongoDB.Bson.Serialization.Attributes;

namespace WeatherBackend.Models
{
    [BsonIgnoreExtraElements]
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
