using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace weekly.Models
{
    public class ToDo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Date { get; set; }
        
        public string Task { get; set; }
        
    }
}