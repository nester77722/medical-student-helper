using MedicalStudentHelper.LocalData.Entities;

namespace MedicalStudentHelper.LocalData.Services;
public interface IUserLocalService
{
    void DeleteUser();
    Task<LocalUser> GetUserAsync();
    Task SaveUserAsync(LocalUser user);
    Task<bool> IsUserLoggedInAsync();
}
