using MedicalStudentHelper.LocalData.Entities;

namespace MedicalStudentHelper.LocalData.Services;
public class UserLocalService : IUserLocalService
{
    private readonly ILocalStorageService _localStorageService;
    private const string UserContextName = "UserContext";
    private const string UserFileName = "localUser.txt";

    public UserLocalService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task SaveUserAsync(LocalUser user)
    {
        await _localStorageService.SaveDataAsync(UserContextName, UserFileName, user);
    }

    public async Task<LocalUser> GetUserAsync()
    {
        if (!_localStorageService.FileExists(UserContextName, UserFileName))
        {
            return null;
        }
        var user = await _localStorageService.LoadDataAsync<LocalUser>(UserContextName, UserFileName);

        if (user == null)
        {
            return null;
        }

        var signedInDateTime = user.LastLoginTime;

        if (signedInDateTime.AddDays(7) < DateTime.Now)
        {            
            DeleteUser();
            user = null;
        }

        return user;
    }

    public void DeleteUser()
    {
        _localStorageService.DeleteData(UserContextName, UserFileName);
    }

    public async Task<bool> IsUserLoggedInAsync()
    {
        if (!_localStorageService.FileExists(UserContextName, UserFileName))
        {
            return false;
        }

        var user = await GetUserAsync();

        if (user == null)
        {
            return false;
        }

        var signedInDateTime = user.LastLoginTime;

        // Проверяем, прошло ли больше 7 дней с момента входа
        if (signedInDateTime.AddDays(7) < DateTime.Now)
        {
            // Если прошло, удаляем пользователя и возвращаем false
            DeleteUser();
            return false;
        }

        // Если нет, возвращаем true
        return true;
    }
}
