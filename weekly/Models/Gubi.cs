using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace weekly.Models
{
    public class Gubi 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int timestamp { get; set; }
    }
}
