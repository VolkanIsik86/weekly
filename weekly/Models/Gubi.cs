using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace weekly.Models
{
    public class Gubi 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        public long Timestamp { get; set; }
    }
}
