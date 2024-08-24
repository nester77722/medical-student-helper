using MedicalStudentHelper.UserData.Models.CreateModels;
using MedicalStudentHelper.UserData.Models.GetModels;

namespace MedicalStudentHelper.UserData.Services.Interfaces;
public interface IUserService
{
    Task<GetUserModel> CreateUserFromGoogleAccountAsync(CreateUserFromGoogleAccountModel createModel);
    Task<GetUserModel> GetUserByGoogleIdAsync(string googleId);
    Task<GetUserModel> GetUserByIdAsync(string id);
}
