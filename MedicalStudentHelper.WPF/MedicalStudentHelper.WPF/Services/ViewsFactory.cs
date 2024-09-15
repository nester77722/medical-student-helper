using MedicalStudentHelper.TestData.Services.Interfaces;
using MedicalStudentHelper.WPF.Services.Interfaces;
using MedicalStudentHelper.WPF.ViewModels.Admin;
using MedicalStudentHelper.WPF.Views;
using MedicalStudentHelper.WPF.Views.Admin;
using Microsoft.Extensions.DependencyInjection;


namespace MedicalStudentHelper.WPF.Services;
public class ViewsFactory
{
    private readonly IServiceProvider _serviceProvider;

    private ViewsFactory _viewsFactory => _serviceProvider.GetRequiredService<ViewsFactory>();
    private ITestService _testService => _serviceProvider.GetRequiredService<ITestService>();
    private INavigationService _navigationService => _serviceProvider.GetRequiredService<INavigationService>();

    public ViewsFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public MainWindow GetMainWindow()
    {
        var window = _serviceProvider.GetRequiredService<MainWindow>();

        return window;
    }

    public ProfileUserControl GetProfilePage()
    {
        var userControl = _serviceProvider.GetRequiredService<ProfileUserControl>();

        return userControl;
    }

    public InsertTestFromKrokLeadUserControl GetInsertTestFromKrokUserControl()
    {
        var userControl = _serviceProvider.GetRequiredService<InsertTestFromKrokLeadUserControl>();

        return userControl;
    }

    public TestCategoriesUserControl GetTestCategoriesUserControl(INavigationService testCategoriesNavigationService)
    {
        var testService = _testService;

        var viewModel = new TestCategoriesViewModel(testService, testCategoriesNavigationService, _viewsFactory);

        var userControl = new TestCategoriesUserControl(viewModel);

        return userControl;
    }

    public TestCategoryDetailsUserControl GetTestCategoryDetailsUserControl(string categoryId, INavigationService testCategoriesNavigationService)
    {
        var viewModel = new TestCategoryDetailsViewModel(categoryId, _testService, testCategoriesNavigationService);

        var userControl = new TestCategoryDetailsUserControl(viewModel);

        return userControl;
    }

    public AdminUserControl GetAdminUserControl()
    {
        var userControl = _serviceProvider.GetRequiredService<AdminUserControl>();

        return userControl;
    }
}
