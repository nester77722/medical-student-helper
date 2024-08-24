using MedicalStudentHelper.API.Entities.TestContextEntities;
using MedicalStudentHelper.API.Models.CreateModels;
using MedicalStudentHelper.API.Models.GetModels;

namespace MedicalStudentHelper.API.Services.Interfaces;

public interface ITestService
{
    Task<GetTestModel> AddTestAsync(CreateTestModel createTestModel);
    Task DeleteTestAsync(string id);
    Task<List<GetAllTestsModel>> GetAllTestsAsync();
    Task<GetTestModel> GetTestByIdAsync(string id);
    Task UpdateTestAsync(string id, Test updatedTest);
}
