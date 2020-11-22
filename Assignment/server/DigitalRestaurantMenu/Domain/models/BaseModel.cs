using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.models
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        // public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        // public string UpdatedBy { get; set; }
    }
}