using MongoDB.Driver;

namespace MedicalStudentHelper.API.Entities.Contexts.UserContex;

public class UserContext
{
    private readonly IMongoDatabase _database;

    public UserContext(UserContextConfiguration settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}
