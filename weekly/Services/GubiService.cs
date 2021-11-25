using MongoDB.Driver;
using System.Collections.Generic;
using weekly.Models;

namespace weekly.Services
{
    public class GubiService
    {
        private readonly IMongoCollection<Gubi> _gubis;
        public GubiService(IWeeklyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _gubis = database.GetCollection<Gubi>(settings.GubiCollectionName);
        }
        public List<Gubi> Get() =>
            _gubis.Find(gubi => true).ToList();

        public Gubi Get(string id) =>
            _gubis.Find<Gubi>(gubi => gubi.Id == id).FirstOrDefault();

        public Gubi Create(Gubi gubi)
        {
            _gubis.InsertOne(gubi);
            return gubi;
        }
        public void Update(string id, Gubi gubiIn)
        {
            
            _gubis.ReplaceOne(gubi => gubi.Id == id, gubiIn);
        }

        public void Remove(Gubi gubiIn) =>
            _gubis.DeleteOne(gubi => gubi.Id == gubiIn.Id);
        public void Remove(string id) =>
            _gubis.DeleteOne(gubi => gubi.Id == id);
    }
}
