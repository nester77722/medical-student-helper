namespace MedicalStudentHelper.LocalData.Services;
public interface ILanguageLocalService
{
    Task<string> GetLanguageAsync();
    Task SaveLanguageAsync(string cultureName);
}
