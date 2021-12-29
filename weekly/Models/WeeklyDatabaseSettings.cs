namespace weekly.Models
{
    public class WeeklyDatabaseSettings : IWeeklyDatabaseSettings
    {
        public string GubiCollectionName { get; set; }
        public string ToDosCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IWeeklyDatabaseSettings
    {
        public string GubiCollectionName { get; set; }
        public string ToDosCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
