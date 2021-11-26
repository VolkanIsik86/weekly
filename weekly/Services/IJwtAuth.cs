namespace weekly.Services
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
