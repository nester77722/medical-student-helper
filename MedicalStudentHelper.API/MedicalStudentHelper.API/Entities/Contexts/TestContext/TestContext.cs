using MedicalStudentHelper.API.Entities.Contexts.UserContext;
using MedicalStudentHelper.API.Entities.TestContextEntities;
using MongoDB.Driver;

namespace MedicalStudentHelper.API.Entities.Contexts.TestContext;

public class TestContext
{
    private readonly IMongoDatabase _database;

    public TestContext(TestContextConfiguration settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<Test> Tests => _database.GetCollection<Test>("Tests");
}
