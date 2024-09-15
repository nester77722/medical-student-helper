using MedicalStudentHelper.LocalData.Entities;
using MedicalStudentHelper.LocalData.Services;
using MedicalStudentHelper.UserData.Services.Interfaces;
using MedicalStudentHelper.WPF.Services.Interfaces;

namespace MedicalStudentHelper.WPF.Services;
public class AppStateService : IAppStateService
{
    private readonly IUserLocalService _userLocalService;
    private bool isUserLoggedIn;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private readonly IUserService _userService;

    public AppStateService(IUserLocalService userLocalService, IUserService userService)
    {
        _userLocalService = userLocalService;
        _userService = userService;
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
        await _semaphore.WaitAsync();

        try
        {
            if (await _userLocalService.IsUserLoggedInAsync())
            {
                var user = await _userLocalService.GetUserAsync();

                var dbUser = await _userService.GetUserByIdAsync(user.Id);

                if (dbUser != null)
                {
                    CurrentUserId = user.Id;
                    IsUserLoggedIn = true;
                }
                else
                {
                    DeleteLoginedUser();
                    CurrentUserId = string.Empty;
                    IsUserLoggedIn = false;
                }
            }
            else
            {
                CurrentUserId = string.Empty;
                IsUserLoggedIn = false;
            }
        }
        finally
        {
            _semaphore.Release();
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
