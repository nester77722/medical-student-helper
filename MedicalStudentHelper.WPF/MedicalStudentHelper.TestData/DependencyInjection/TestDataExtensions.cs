using MedicalStudentHelper.TestData.Services;
using MedicalStudentHelper.TestData.Services.Interfaces;
using MedicalStudentHelper.TestData.TestContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MedicalStudentHelper.TestData.DependencyInjection;
public static class TestDataExtensions
{
    public static IHostBuilder AddTestContext(this IHostBuilder builder, string sectionName = "TestContextConfiguration")
    {
        builder.ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration;
            services.Configure<TestContextConfiguration>(options =>
            {
                configuration.GetSection(sectionName).Bind(options);
            });
        });

        builder.ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration;
            services.AddTransient<TestContext.TestContext>();
        });

        return builder;
    }

    public static IHostBuilder AddTestService(this IHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            services.AddTransient<ITestService, TestService>();
        });

        return builder;
    }
}
