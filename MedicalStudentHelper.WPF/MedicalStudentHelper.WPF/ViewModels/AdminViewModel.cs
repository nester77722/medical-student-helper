using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalStudentHelper.WPF.Services;
using MedicalStudentHelper.WPF.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace MedicalStudentHelper.WPF.ViewModels;

public partial class AdminViewModel : ObservableObject
{
    private readonly ViewsFactory _viewsFactory;
    private readonly INavigationService _testCategoriesNavigationService;

    public AdminViewModel(ViewsFactory viewsFactory,
                         [FromKeyedServices(NavigationServiceKeys.AdminCategoriesNavigationServiceKey)]INavigationService testCategoriesNavigationService)
    {
        _viewsFactory = viewsFactory;
        _testCategoriesNavigationService = testCategoriesNavigationService;

        _testCategoriesNavigationService.CurrentContentChanged += TestCategoriesNavigationServiceCurrentContentChanged;

        var testCategoriesView = viewsFactory.GetTestCategoriesUserControl(testCategoriesNavigationService);
        _testCategoriesNavigationService.NavigateTo(testCategoriesView);
    }

    private void TestCategoriesNavigationServiceCurrentContentChanged(UserControl newContent)
    {
        CurrentContent = newContent;
    }

    [ObservableProperty]
    private UserControl _currentContent;

    [RelayCommand]
    private void RedirectToInsertTestFromKrokLeadView()
    {
        CurrentContent = _viewsFactory.GetInsertTestFromKrokUserControl();
    }

    [RelayCommand]
    private void RedirectToTestCategoriesView()
    {
        CurrentContent = _testCategoriesNavigationService.CurrentContent;
    }
}
