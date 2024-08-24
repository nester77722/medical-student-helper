using MedicalStudentHelper.LocalData.Services;
using MedicalStudentHelper.WPF.Helpers;
using MedicalStudentHelper.WPF.Services;
using MedicalStudentHelper.WPF.Services.Interfaces;
using MedicalStudentHelper.WPF.Views;
using System.Windows;

namespace MedicalStudentHelper.WPF;
public class App : Application
{
    private readonly MainWindow _mainWindow;
    private readonly IAppStateService _appStateService;
    private readonly IUserLocalService _userLocalService;
    private readonly LanguageManager _languageManager;

    public App(ViewsFactory viewsFactory, IAppStateService appStateService, IUserLocalService userLocalService, LanguageManager languageManager)
    {
        _appStateService = appStateService;
        _userLocalService = userLocalService;
        _languageManager = languageManager;

        //Resources.MergedDictionaries.Clear();

        //Resources.MergedDictionaries.Add(new ResourceDictionary()
        //{
        //    Source = new Uri($"Assets/Localization/Resources.ru-RU.xaml", UriKind.Relative)
        //});

        _mainWindow = viewsFactory.GetMainWindow();
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        await CheckSavedLanguageSettings();
        _mainWindow.Show();
        
        base.OnStartup(e);
    }

    private async Task CheckSavedLanguageSettings()
    {
        await _languageManager.CheckSavedLanguageAsync();
    }
}
