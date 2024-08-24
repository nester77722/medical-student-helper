using MedicalStudentHelper.UserData.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MedicalStudentHelper.UserData.UserContext;

public class UserContext
{
    private readonly IMongoDatabase _database;

    public UserContext(IOptions<UserContextConfiguration> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);

        CreateIndexes();
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

    private void CreateIndexes()
    {
        var indexKeysDefinition = Builders<User>.IndexKeys.Ascending(u => u.GoogleId);
        var indexModel = new CreateIndexModel<User>(indexKeysDefinition, new CreateIndexOptions { Unique = true });
        Users.Indexes.CreateOne(indexModel);
    }
}
