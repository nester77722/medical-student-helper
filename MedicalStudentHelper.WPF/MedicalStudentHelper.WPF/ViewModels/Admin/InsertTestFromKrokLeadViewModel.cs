using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalStudentHelper.TestData.Models.CreateModels;
using MedicalStudentHelper.TestData.Models.GetModels;
using MedicalStudentHelper.TestData.Services.Interfaces;
using MedicalStudentHelper.WPF.Models;
using Newtonsoft.Json;

namespace MedicalStudentHelper.WPF.ViewModels.Admin;
public partial class InsertTestFromKrokLeadViewModel : ObservableObject
{
    private readonly ITestService _testService;
    private readonly IMapper _mapper;

    public InsertTestFromKrokLeadViewModel(ITestService testService)
    {
        _testService = testService;

        Json = string.Empty;
        TestCategoryModels = new List<TestCategoryModel>();

        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<GetTestCategoryModel, TestCategoryModel>();
        });

        _mapper = mapperConfiguration.CreateMapper();
        LoadTestCategories();
    }

    private void LoadTestCategories()
    {
        _testService.GetAllTestCategoriesAsync().ContinueWith(result =>
        {
            TestCategoryModels = _mapper.Map<List<TestCategoryModel>>(result.Result);
        });
    }

    [ObservableProperty]
    private List<TestCategoryModel> _testCategoryModels;

    [ObservableProperty]
    private TestCategoryModel? _selectedCategory;

    [ObservableProperty]
    private string _json;

    [RelayCommand]
    private async Task AddTestAsync()
    {
        var jsonData = JsonConvert.DeserializeObject<List<KrokLeadTestModel>>(Json);

        var createTestModel = new CreateTestModel
        {
            Name = "Крок 2 другий день",
            CategoryId = SelectedCategory.Id,
            Description = "Крок 2 другий день",
            Questions = jsonData.Select(q => new CreateQuestionModel
            {
                Text = q.Question,
                Variants = q.Variants,
                CorrectAnswer = q.Answer
            }).ToList()
        };

        await _testService.AddTestAsync(createTestModel);

        var tests = await _testService.GetTestsByCategoryIdAsync(SelectedCategory.Id);

        var testWithQuestions = await _testService.GetTestWithQuestionsAsync(tests.First().Id);
    }
}
