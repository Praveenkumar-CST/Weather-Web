using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherBackend.Models
{
    public class UserFavorites
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; } // If you track users
        public string CityName { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
    }
}
