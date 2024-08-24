using MedicalStudentHelper.TestData.Entities;
using MedicalStudentHelper.TestData.Models.CreateModels;
using MedicalStudentHelper.TestData.Models.GetModels;
using MedicalStudentHelper.TestData.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MedicalStudentHelper.TestData.Services;

public class TestService : ITestService
{
    private readonly TestContext.TestContext _testContext;

    public TestService(TestContext.TestContext testContext)
    {
        _testContext = testContext;
    }

    public async Task<List<GetAllTestsModel>> GetAllTestsAsync()
    {
        var tests = await _testContext.Tests.Find(FilterDefinition<Test>.Empty).ToListAsync();

        var result = tests.Select(t => new GetAllTestsModel
        {
            Id = t.Id.ToString(),
            Name = t.Name
        }).ToList();

        return result;
    }

    public async Task<GetTestModel> GetTestByIdAsync(string id)
    {
        ObjectId objectId = ObjectId.Parse(id);

        var filter = Builders<Test>.Filter.Eq(t => t.Id, objectId);
        var test = await _testContext.Tests.Find(filter).FirstOrDefaultAsync();

        var result = new GetTestModel
        {
            Id = test.Id.ToString(),
            Name = test.Name,
            Description = test.Description,
        };

        return result;
    }

    public async Task<GetTestModel> AddTestAsync(CreateTestModel createTestModel)
    {
        var newTest = new Test
        {
            Name = createTestModel.Name,
            Description = createTestModel.Description,
        };

        await _testContext.Tests.InsertOneAsync(newTest);

        var result = new GetTestModel
        {
            Id = newTest.Id.ToString(),
            Name = newTest.Name,
            Description = newTest.Description,
        };

        return result;
    }

    public async Task UpdateTestAsync(string id, Test updatedTest)
    {
        ObjectId objectId = ObjectId.Parse(id);

        var filter = Builders<Test>.Filter.Eq(t => t.Id, objectId);

        await _testContext.Tests.ReplaceOneAsync(filter, updatedTest);
    }

    public async Task DeleteTestAsync(string id)
    {
        ObjectId objectId = ObjectId.Parse(id);

        var filter = Builders<Test>.Filter.Eq(t => t.Id, objectId);
        await _testContext.Tests.DeleteOneAsync(filter);
    }
}
