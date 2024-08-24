using MedicalStudentHelper.LocalData.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStudentHelper.LocalData.DependencyInjection;
public static class LocalDataExtension
{
    public static IHostBuilder AddLocalStorageService(this IHostBuilder builder)
    {
        builder.AddEncryptionService();

        builder.ConfigureServices((context, services) =>
        {
            var localStorageServiceRegistered = services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(ILocalStorageService));

            if (!localStorageServiceRegistered)
            {
                services.AddTransient<ILocalStorageService, LocalStorageService>();
            }
        });

        return builder;
    }

    public static IHostBuilder AddEncryptionService(this IHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            var encryptionServiceRegistered = services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IEncryptionService));

            if (!encryptionServiceRegistered)
            {
                services.AddTransient<IEncryptionService, EncryptionService>();
            }
        });

        return builder;
    }

    public static IHostBuilder AddLocalUserService(this IHostBuilder builder)
    {
        builder.AddLocalStorageService();

        builder.ConfigureServices((context, services) =>
        {
            services.AddTransient<IUserLocalService, UserLocalService>();
        });

        return builder;
    }

    public static IHostBuilder AddLocalLanguageService(this IHostBuilder builder)
    {
        builder.AddLocalStorageService();

        builder.ConfigureServices((context, services) =>
        {
            services.AddTransient<ILanguageLocalService, LanguageLocalService>();
        });

        return builder;
    }
}
