using MedicalStudentHelper.UserData.Services;
using MedicalStudentHelper.UserData.Services.Interfaces;
using MedicalStudentHelper.UserData.UserContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MedicalStudentHelper.UserData.DependencyInjection;
public static class UserDataExtensions
{
    public static IHostBuilder AddUserContext(this IHostBuilder builder, string sectionName = "UserContextConfiguration")
    {
        builder.ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration;
            services.Configure<UserContextConfiguration>(options =>
            {
                configuration.GetSection(sectionName).Bind(options);
            });
        });

        builder.ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration;
            services.AddTransient<UserContext.UserContext>();
        });

        return builder;
    }

    public static IHostBuilder AddUserService(this IHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            var userContextRegistered = services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(UserContext.UserContext));

            if (!userContextRegistered)
            {
                builder.AddUserContext();
            }

            services.AddTransient<IUserService, UserService>();
        });

        return builder;
    }
}
