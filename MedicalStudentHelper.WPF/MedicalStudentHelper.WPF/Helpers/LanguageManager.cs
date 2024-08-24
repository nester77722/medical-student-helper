using MedicalStudentHelper.LocalData.Services;
using System.Globalization;
using System.Windows;

namespace MedicalStudentHelper.WPF.Helpers;

public class LanguageManager
{
    private readonly ILanguageLocalService _languageLocalService;
    private const string DefaultCultureInfo = "ru-RU";

    public LanguageManager(ILanguageLocalService languageLocalService)
    {
        _languageLocalService = languageLocalService;
    }

    public async Task ChangeLanguageAsync(string cultureCode)
    {
        var culture = new CultureInfo(cultureCode);
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        var dict = new ResourceDictionary
        {
            Source = new Uri($"Assets/Localization/Resources.{cultureCode}.xaml", UriKind.Relative)
        };

        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(dict);

        await _languageLocalService.SaveLanguageAsync(culture.Name);
    }

    public async Task CheckSavedLanguageAsync()
    {
        var cultureName = await _languageLocalService.GetLanguageAsync();

        if (string.IsNullOrEmpty(cultureName))
        {
            var culture = new CultureInfo(DefaultCultureInfo);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            await _languageLocalService.SaveLanguageAsync(culture.Name);

            var dict = new ResourceDictionary
            {
                Source = new Uri($"Assets/Localization/Resources.{DefaultCultureInfo}.xaml", UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
        else
        {
            var dict = new ResourceDictionary
            {
                Source = new Uri($"Assets/Localization/Resources.{cultureName}.xaml", UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

    }
}
