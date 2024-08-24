namespace MedicalStudentHelper.LocalData.Services;
public class LanguageLocalService : ILanguageLocalService
{
    private readonly ILocalStorageService _localStorageService;
    private const string LanguageContextName = "LanguageContext";
    private const string LanguageFileName = "language.txt";

    public LanguageLocalService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task SaveLanguageAsync(string cultureName)
    {
        await _localStorageService.SaveDataAsync(LanguageContextName, LanguageFileName, cultureName);
    }

    public async Task<string> GetLanguageAsync()
    {
        if (!_localStorageService.FileExists(LanguageContextName, LanguageFileName))
        {
            return null;
        }
        var cultureInfo = await _localStorageService.LoadDataAsync<string>(LanguageContextName, LanguageFileName);

        if (string.IsNullOrEmpty(cultureInfo))
        {
            return null;
        }

        return cultureInfo;
    }
}
