using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalStudentHelper.TestData.Models.CreateModels;
using MedicalStudentHelper.TestData.Models.GetModels;
using MedicalStudentHelper.TestData.Services.Interfaces;
using MedicalStudentHelper.WPF.Models;
using MedicalStudentHelper.WPF.Services;
using MedicalStudentHelper.WPF.Services.Interfaces;
using System.Windows;

namespace MedicalStudentHelper.WPF.ViewModels.Admin;
public partial class TestCategoriesViewModel : ObservableObject
{
    private readonly ITestService _testService;
    private readonly IMapper _mapper;
    private readonly INavigationService _navigationService;
    private readonly ViewsFactory _viewFactory;

    private List<TestCategoryModel> _originalCategoryModels;
    public TestCategoriesViewModel(ITestService testService, INavigationService navigationService, ViewsFactory viewFactory)
    {
        _testService = testService;

        _navigationService = navigationService;
        _navigationService.CurrentContentChanged += NavigationServiceCurrentContentChanged;

        _viewFactory = viewFactory;

        var mapperConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<GetTestCategoryModel, TestCategoryModel>();
            config.CreateMap<TestCategoryModel, UpdateTestCategoryModel>();
            config.CreateMap<TestCategoryModel, CreateTestCategoryModel>();
        });

        _mapper = mapperConfig.CreateMapper();

        CategoryModels = new();

        LoadCategories();
    }

    private void NavigationServiceCurrentContentChanged(System.Windows.Controls.UserControl obj)
    {
        GoBackVisibility = _navigationService.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
    }

    [ObservableProperty]
    private List<TestCategoryModel> _categoryModels;

    [ObservableProperty]
    private Visibility _goBackVisibility = Visibility.Hidden;

    [RelayCommand]
    private void RedirectToCategory(TestCategoryModel category)
    {
        if (category != null && !string.IsNullOrEmpty(category.Id))
        {
            var view = _viewFactory.GetTestCategoryDetailsUserControl(category.Id, _navigationService);
            _navigationService.NavigateTo(view);
        }
    }

    [RelayCommand]
    private void CancelChanges()
    {
        LoadCategories();
    }

    [RelayCommand]
    private async Task SaveChangesAsync()
    {
        // Получаем измененные категории
        var modifiedCategories = GetModifiedCategories();

        if (modifiedCategories.Any())
        {
            foreach (var category in modifiedCategories)
            {
                if (string.IsNullOrWhiteSpace(category.Id))
                {
                    // Если у категории нет Id, создаём новую категорию
                    var newCategory = _mapper.Map<CreateTestCategoryModel>(category);

                    await _testService.CreateCategoryAsync(newCategory);
                }
                else
                {
                    // Если у категории есть Id, обновляем существующую категорию
                    var updatedCategory = new UpdateTestCategoryModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        Year = category.Year
                    };
                    await _testService.UpdateCategoryAsync(updatedCategory);
                }
            }
        }

        // Перезагружаем категории
        LoadCategories();
    }

    private void LoadCategories()
    {
        _testService.GetAllTestCategoriesAsync().ContinueWith(r =>
        {
            var result = _mapper.Map<List<TestCategoryModel>>(r.Result);

            result.Insert(0, new TestCategoryModel { Name = string.Empty, Description = string.Empty, Year = DateTime.Now.Year });

            _originalCategoryModels = new List<TestCategoryModel>(result.Select(x => (TestCategoryModel)x.Clone()));
            CategoryModels = new List<TestCategoryModel>(result);
        });
    }

    private List<TestCategoryModel> GetModifiedCategories()
    {
        var modifiedCategories = new List<TestCategoryModel>();

        for (int i = 0; i < CategoryModels.Count; i++)
        {
            var original = _originalCategoryModels[i];
            var current = CategoryModels[i];

            if (!original.Equals(current))
            {
                modifiedCategories.Add(current);
            }
        }

        return modifiedCategories;
    }
}
