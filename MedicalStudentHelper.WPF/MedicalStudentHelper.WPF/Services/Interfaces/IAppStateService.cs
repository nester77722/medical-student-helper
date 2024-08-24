using MedicalStudentHelper.LocalData.Entities;

namespace MedicalStudentHelper.WPF.Services.Interfaces;
public interface IAppStateService
{
    bool IsUserLoggedIn { get; }
    string CurrentUserId { get; }

    event EventHandler StateChanged;

    void DeleteLoginedUser();
    Task SaveLoginedUserAsync(LocalUser localUser);
    void StartCheckingUserLoginState();
}
