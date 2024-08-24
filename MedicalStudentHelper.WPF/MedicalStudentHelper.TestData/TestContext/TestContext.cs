using MedicalStudentHelper.TestData.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MedicalStudentHelper.TestData.TestContext;

public class TestContext
{
    private readonly IMongoDatabase _database;

    public TestContext(IOptions<TestContextConfiguration> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<Test> Tests => _database.GetCollection<Test>("Tests");
}
