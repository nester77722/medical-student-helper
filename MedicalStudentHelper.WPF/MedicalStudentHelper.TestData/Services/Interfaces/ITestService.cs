using MedicalStudentHelper.TestData.Entities;
using MedicalStudentHelper.TestData.Models.CreateModels;
using MedicalStudentHelper.TestData.Models.GetModels;

namespace MedicalStudentHelper.TestData.Services.Interfaces;

public interface ITestService
{
    Task<GetTestModel> AddTestAsync(CreateTestModel createTestModel);
    Task DeleteTestAsync(string id);
    Task<List<GetAllTestsModel>> GetAllTestsAsync();
    Task<GetTestModel> GetTestByIdAsync(string id);
    Task UpdateTestAsync(string id, Test updatedTest);
}
