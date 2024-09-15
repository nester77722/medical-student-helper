using MedicalStudentHelper.WPF.Helpers;
using MedicalStudentHelper.WPF.Services;
using MedicalStudentHelper.WPF.Services.Interfaces;
using MedicalStudentHelper.WPF.ViewModels;
using MedicalStudentHelper.WPF.ViewModels.Admin;
using MedicalStudentHelper.WPF.Views;
using MedicalStudentHelper.WPF.Views.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace MedicalStudentHelper.WPF;
internal static class HostBuilderExtensions
{
    internal static IHostBuilder ConfigureConfiguration(this IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((hostContext, configBuilder) =>
        {
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var config = configBuilder.Build();
        });

        return builder;
    }

    internal static IHostBuilder AddAppStateService(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IAppStateService, AppStateService>();
        });

        return builder;
    }

    internal static IHostBuilder AddViewModels(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<AdminViewModel>();
            services.AddTransient<InsertTestFromKrokLeadViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<ProfilePageViewModel>();
            services.AddSingleton<TestCategoriesViewModel>();
            //services.AddTransient<InsertIdsProgressViewModel>();
        });

        return builder;
    }

    internal static IHostBuilder AddViews(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<ProfileUserControl>();
            services.AddTransient<InsertTestFromKrokLeadUserControl>();
            services.AddSingleton<AdminUserControl>();
            services.AddSingleton<TestCategoriesUserControl>();
            //services.AddSingleton<PivotalSalesforceIdsPage>();
            //services.AddTransient<AccountDetailsWindow>();
            //services.AddTransient<InsertIdsProgressWindow>();
            services.AddSingleton<ViewsFactory>();
        });

        return builder;
    }

    internal static IHostBuilder AddServices(this IHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            services.AddSingleton<App>();
            services.AddSingleton<LanguageManager>();
            services.AddTransient<INavigationService, NavigationService>();
            services.AddKeyedSingleton<INavigationService, NavigationService>(NavigationServiceKeys.AdminCategoriesNavigationServiceKey);
            services.AddKeyedSingleton<INavigationService, NavigationService>(NavigationServiceKeys.CategoriesNavigationServiceKey);
            //services.AddTransient<IPivotalService, PivotalService>();
        });

        return builder;
    }

    internal static IHostBuilder AddMapper(this IHostBuilder builder)
    {
        //var mapperConfig = new MapperConfiguration(mc =>
        //{
        //    mc.AddProfiles(SalesforceEntitiesToSqlEntitiesMapperSettings.Profiles);
        //});

        //IMapper mapper = mapperConfig.CreateMapper();

        //builder.ConfigureServices(services =>
        //{
        //    services.AddSingleton(mapper);
        //});

        return builder;
    }
}

