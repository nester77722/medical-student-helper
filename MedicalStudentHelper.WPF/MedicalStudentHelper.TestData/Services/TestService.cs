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

    public async Task<List<GetTestCategoryModel>> GetAllTestCategoriesAsync()
    {
        // Получаем все категории тестов из базы данных
        var categories = await _testContext.TestCategories.Find(FilterDefinition<TestCategory>.Empty).ToListAsync();

        // Преобразуем результаты в модель GetTestCategoryModel
        var result = categories.Select(category => new GetTestCategoryModel
        {
            Id = category.Id.ToString(),
            Name = category.Name,
            Description = category.Description,
            Year = category.Year
        }).ToList();

        return result;
    }

    public async Task<List<GetTestWithoutQuestionsModel>> GetTestsByCategoryIdAsync(string categoryId)
    {
        var categoryFilter = Builders<TestCategory>.Filter.Eq(t => t.Id, ObjectId.Parse(categoryId));

        var category = await _testContext.TestCategories.Find(categoryFilter).FirstOrDefaultAsync();

        if (category is null)
        {
            throw new ArgumentException($"Category with id {categoryId} was not found.");
        }

        var testFilter = Builders<Test>.Filter.Eq(t => t.CategoryId, category.Id);
        var testProjection = Builders<Test>.Projection.Exclude(t => t.Questions);

        var tests = await _testContext.Tests.Find(testFilter).Project<Test>(testProjection).ToListAsync();

        var result = tests.Select(t => new GetTestWithoutQuestionsModel
        {
            Id = t.Id.ToString(),
            Name = t.Name,
            Description = t.Description,
        }).ToList();

        return result;
    }

    public async Task<GetTestCategoryModel> CreateCategoryAsync(CreateTestCategoryModel createTestCategoryModel)
    {
        var newCategory = new TestCategory
        {
            Name = createTestCategoryModel.Name,
            Description = createTestCategoryModel.Description,
            Year = createTestCategoryModel.Year,
        };

        await _testContext.TestCategories.InsertOneAsync(newCategory);

        var result = new GetTestCategoryModel
        {
            Name = newCategory.Name,
            Year = newCategory.Year,
            Description = newCategory.Description,
            Id = newCategory.Id.ToString()
        };

        return result;
    }

    public async Task UpdateCategoryAsync(UpdateTestCategoryModel updateTestCategoryModel)
    {
        var filter = Builders<TestCategory>.Filter.Eq(c => c.Id, ObjectId.Parse(updateTestCategoryModel.Id));
        var update = Builders<TestCategory>.Update
            .Set(c => c.Name, updateTestCategoryModel.Name)
            .Set(c => c.Description, updateTestCategoryModel.Description)
            .Set(c => c.Year, updateTestCategoryModel.Year);

        await _testContext.TestCategories.UpdateOneAsync(filter, update);
    }

    public async Task<GetTestWithQuestionsModel> GetTestWithQuestionsAsync(string id)
    {
        ObjectId testId = ObjectId.Parse(id);
        var testFilter = Builders<Test>.Filter.Eq(t => t.Id, testId);

        var testWithQuestions = await _testContext.Tests.Find(testFilter).FirstOrDefaultAsync();

        if (testWithQuestions is null)
        {
            throw new ArgumentException($"Test with id {id} was not found.");
        }

        var result = new GetTestWithQuestionsModel
        {
            Id = testWithQuestions.Id.ToString(),
            Name = testWithQuestions.Name,
            Description = testWithQuestions.Description,
            Questions = testWithQuestions.Questions.Select(q => new GetQuestionModel
            {
                Id = q.Id.ToString(),
                Text = q.Text,
                CorrectAnswer = q.CorrectAnswer,
                Variants = q.Variants
            }).ToList()
        };

        return result;
    }

    public async Task<GetTestWithQuestionsModel> AddTestAsync(CreateTestModel createTestModel)
    {
        var categoryId = ObjectId.Parse(createTestModel.CategoryId);

        var categoryFilter = Builders<TestCategory>.Filter.Eq(t => t.Id, categoryId);

        var category = await _testContext.TestCategories.Find(categoryFilter).FirstOrDefaultAsync();

        if (category is null)
        {
            throw new ArgumentException($"Category with id {createTestModel.CategoryId} was not found.");
        }

        var newTest = new Test
        {
            Name = createTestModel.Name,
            Description = createTestModel.Description,
            CategoryId = categoryId,
            Questions = createTestModel.Questions.Select(q => new Question
            {
                Id = ObjectId.GenerateNewId(),
                Variants = q.Variants,
                CorrectAnswer = q.CorrectAnswer,
                Text = q.Text,
            }).ToList()
        };

        await _testContext.Tests.InsertOneAsync(newTest);

        var result = new GetTestWithQuestionsModel
        {
            Id = newTest.Id.ToString(),
            Name = newTest.Name,
            Description = newTest.Description,
            Questions = newTest.Questions.Select(q => new GetQuestionModel
            {
                Id = q.Id.ToString(),
                CorrectAnswer = q.CorrectAnswer,
                Text = q.Text,
                TestId = newTest.Id.ToString(),
                Variants = q.Variants
            }).ToList()
        };

        return result;
    }

    public async Task UpdateTestAsync(string id, Test updatedTest)
    {
        //var categoryId = ObjectId.Parse(updatedTest.CategoryId);

        var categoryFilter = Builders<TestCategory>.Filter.Eq(t => t.Id, updatedTest.CategoryId);

        var category = await _testContext.TestCategories.Find(categoryFilter).FirstOrDefaultAsync();

        if (category is null)
        {
            throw new ArgumentException($"Category with id {updatedTest.CategoryId} was not found.");
        }

        ObjectId objectId = ObjectId.Parse(id);

        var filter = Builders<Test>.Filter.Eq(t => t.Id, objectId);

        await _testContext.Tests.ReplaceOneAsync(filter, updatedTest);
    }

    public async Task DeleteTestAsync(string id)
    {
        ObjectId testId = ObjectId.Parse(id);

        // Фильтр для теста
        var testFilter = Builders<Test>.Filter.Eq(t => t.Id, testId);

        // Проверяем наличие теста
        var test = await _testContext.Tests.Find(testFilter).FirstOrDefaultAsync();
        if (test is null)
        {
            throw new ArgumentException($"Test with id {id} was not found.");
        }

        // Удаляем сам тест
        await _testContext.Tests.DeleteOneAsync(testFilter);
    }
}
