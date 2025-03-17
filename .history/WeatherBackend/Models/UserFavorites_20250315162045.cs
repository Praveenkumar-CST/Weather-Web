using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WeatherBackend.Models
{
    public class UserFavorites
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } // Allow MongoDB to generate Id

        public string? UserId { get; set; } // Make UserId optional

        [BsonElement("cityName")]
        public string CityName { get; set; } = string.Empty; // City name is required

        [BsonElement("addedOn")]
        public DateTime AddedOn { get; set; } = DateTime.UtcNow; // Automatically set
    }
}
