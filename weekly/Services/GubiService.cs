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

        public Gubi AddSmiley()
        {
            Gubi temp = _gubis.Find(gubi => gubi.Name == "smiley").FirstOrDefault();

            if (temp.Timestamp < 5)
            {
                temp.Timestamp++;
            }

            _gubis.ReplaceOne(gubi => gubi.Name == temp.Name, temp);
            return temp;
        }
        public Gubi ClearSmiley()
        {
            Gubi temp = _gubis.Find(gubi => gubi.Name == "smiley").FirstOrDefault();

            temp.Timestamp = 0;

            _gubis.ReplaceOne(gubi => gubi.Name == temp.Name, temp);
            return temp;
        }
        public Gubi CreateSmiley()
        {
            Gubi gubi = new Gubi();
            gubi.Name = "smiley";
            gubi.Timestamp = 0;
            _gubis.InsertOne(gubi);
            return gubi;
        }

        public Gubi GetSmiley()
        {
            Gubi gubi = _gubis.Find<Gubi>(gubi => gubi.Name == "smiley").FirstOrDefault();
            return gubi;
        }
        
        
    }
}
