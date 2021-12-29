using System.Collections.Generic;
using MongoDB.Driver;
using weekly.Models;

namespace weekly.Services
{
    public class ToDoService
    {
        private readonly IMongoCollection<ToDo> _toDos;

        public ToDoService(IWeeklyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _toDos = database.GetCollection<ToDo>(settings.ToDosCollectionName);
        }

        public List<ToDo> Get() =>
            _toDos.Find(todo => true).ToList();

        public ToDo Get(string id) =>
            _toDos.Find<ToDo>(todo => todo.Id == id).FirstOrDefault();

        public ToDo Create(ToDo todo)
        {
            _toDos.InsertOne(todo);
            return todo;
        }

        public void Update(string id, ToDo todoIn)
        {
            _toDos.ReplaceOne(todo => todo.Id == id, todoIn);
        }

        public void Remove(string id) =>
            _toDos.DeleteOne(todo => todo.Id == id);
    }
}