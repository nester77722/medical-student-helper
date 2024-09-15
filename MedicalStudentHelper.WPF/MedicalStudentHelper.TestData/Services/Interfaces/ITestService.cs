using MedicalStudentHelper.TestData.Entities;
using MedicalStudentHelper.TestData.Models.CreateModels;
using MedicalStudentHelper.TestData.Models.GetModels;

namespace MedicalStudentHelper.TestData.Services.Interfaces;

public interface ITestService
{
    Task<GetTestWithQuestionsModel> AddTestAsync(CreateTestModel createTestModel);
    Task<GetTestCategoryModel> CreateCategoryAsync(CreateTestCategoryModel createTestCategoryModel);
    Task DeleteTestAsync(string id);
    Task<List<GetTestCategoryModel>> GetAllTestCategoriesAsync();
    Task<List<GetTestWithoutQuestionsModel>> GetTestsByCategoryIdAsync(string categoryId);
    Task<GetTestWithQuestionsModel> GetTestWithQuestionsAsync(string id);
    Task UpdateCategoryAsync(UpdateTestCategoryModel updateTestCategoryModel);
    Task UpdateTestAsync(string id, Test updatedTest);
}
