using MongoDB.Driver;
using System.Collections.Generic;
using weekly.Models;

namespace weekly.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        public UserService (IWeeklyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }
        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _users.InsertOne(user);
            return user;
        }
        public void Update(string id, User userIn)
        {
            userIn.Password = BCrypt.Net.BCrypt.HashPassword(userIn.Password);
            _users.ReplaceOne(user => user.Id == id, userIn);
        }

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);
        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);
        public User Verify(string name, string password)
        {
            User user = _users.Find<User>(user => user.Email == name).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                return user;
            else
                return null;
        }
    }
}
