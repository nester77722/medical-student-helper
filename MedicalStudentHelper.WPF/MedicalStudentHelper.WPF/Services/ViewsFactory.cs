using MedicalStudentHelper.WPF.Views;
using Microsoft.Extensions.DependencyInjection;


namespace MedicalStudentHelper.WPF.Services;
public class ViewsFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ViewsFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public MainWindow GetMainWindow()
    {
        var window = _serviceProvider.GetRequiredService<MainWindow>();

        return window;
    }

    public ProfilePage GetProfilePage()
    {
        var page = _serviceProvider.GetRequiredService<ProfilePage>();

        return page;
    }
}
