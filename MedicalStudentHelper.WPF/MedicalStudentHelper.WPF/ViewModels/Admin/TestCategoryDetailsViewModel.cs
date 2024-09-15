using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalStudentHelper.TestData.Services.Interfaces;
using MedicalStudentHelper.WPF.Models;
using MedicalStudentHelper.WPF.Services.Interfaces;
using System.Windows;

namespace MedicalStudentHelper.WPF.ViewModels.Admin;
public partial class TestCategoryDetailsViewModel : ObservableObject
{
    private readonly string _categoryId;
    private readonly ITestService _testService;
    private readonly INavigationService _navigationService;

    public TestCategoryDetailsViewModel(string categoryId, ITestService testService, INavigationService navigationService)
    {
        _categoryId = categoryId;
        _testService = testService;
        _navigationService = navigationService;

        _navigationService.CurrentContentChanged += NavigationServiceCurrentContentChanged;

        LoadCategory();
    }

    private void NavigationServiceCurrentContentChanged(System.Windows.Controls.UserControl obj)
    {
        GoBackVisibility = _navigationService.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
    }

    [ObservableProperty]
    private List<TestModel> _tests;

    [ObservableProperty]
    private Visibility _goBackVisibility = Visibility.Hidden;

    private void LoadCategory()
    {
        _testService.GetTestsByCategoryIdAsync(_categoryId).ContinueWith(r =>
        {
            var result = r.Result;

            Tests = result.Select(t => new TestModel
            {
                Id = t.Id,
                Description = t.Description,
                Name = t.Name,
            }).ToList();
        });
    }

    [RelayCommand]
    private void GoBack()
    {
        _navigationService.GoBack();
    }
}
