using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WeatherBackend.Models
{
    public class UserFavorites
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } // Use ObjectId instead of string

        public string? UserId { get; set; }

        [BsonElement("cityName")]
        public string CityName { get; set; } = string.Empty;

        [BsonElement("addedOn")]
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
    }
}
