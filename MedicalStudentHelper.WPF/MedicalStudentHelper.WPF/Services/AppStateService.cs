using MedicalStudentHelper.LocalData.Entities;
using MedicalStudentHelper.LocalData.Services;
using MedicalStudentHelper.WPF.Services.Interfaces;

namespace MedicalStudentHelper.WPF.Services;
public class AppStateService : IAppStateService
{
    private readonly IUserLocalService _userLocalService;
    private bool isUserLoggedIn;

    public AppStateService(IUserLocalService userLocalService)
    {
        _userLocalService = userLocalService;
    }

    public event EventHandler StateChanged;

    public string CurrentUserId { get; private set; }
    public bool IsUserLoggedIn
    {
        get => isUserLoggedIn;
        private set
        {
            isUserLoggedIn = value;
            OnStateChanged();
        }
    }

    public void StartCheckingUserLoginState()
    {
        _ = CheckUserLoginStateAsync();
    }

    private async Task CheckUserLoginStateAsync()
    {
        if (await _userLocalService.IsUserLoggedInAsync())
        {
            var user = await _userLocalService.GetUserAsync();
            CurrentUserId = user.Id;
            IsUserLoggedIn = true;
        }
        else
        {
            IsUserLoggedIn = false;
            CurrentUserId = string.Empty;
        }
    }

    protected virtual void OnStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void DeleteLoginedUser()
    {
        CurrentUserId = string.Empty;

        _userLocalService.DeleteUser();

        IsUserLoggedIn = false;
    }

    public async Task SaveLoginedUserAsync(LocalUser localUser)
    {
        CurrentUserId = localUser.Id;

        await _userLocalService.SaveUserAsync(localUser);

        IsUserLoggedIn = true;
    }
}
