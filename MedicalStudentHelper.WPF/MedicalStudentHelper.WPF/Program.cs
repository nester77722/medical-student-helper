using MedicalStudentHelper.LocalData.DependencyInjection;
using MedicalStudentHelper.TestData.DependencyInjection;
using MedicalStudentHelper.UserData.DependencyInjection;
using MedicalStudentHelper.WPF.Helpers;
using MedicalStudentHelper.WPF.Services.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;


namespace MedicalStudentHelper.WPF;
public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                 .WriteTo.File(new JsonFormatter(), "log.json", LogEventLevel.Verbose, fileSizeLimitBytes: 4096)
                 .WriteTo.Sink<MessageBoxSerilogSink>(LogEventLevel.Warning)
                 .CreateLogger();

        Log.Information("Starting up...");

        var hostBuilder = new HostBuilder().ConfigureDefaults(args);
        hostBuilder.ConfigureConfiguration();
        hostBuilder.AddViewModels();
        hostBuilder.AddViews();
        hostBuilder.AddServices();
        hostBuilder.AddMapper();

        hostBuilder.AddTestContext();
        hostBuilder.AddTestService();

        hostBuilder.AddEncryptionService();
        hostBuilder.AddLocalStorageService();
        hostBuilder.AddLocalUserService();
        hostBuilder.AddLocalLanguageService();

        hostBuilder.AddUserContext();
        hostBuilder.AddUserService();

        hostBuilder.AddAppStateService();

        hostBuilder.ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration;
            services.Configure<GoogleAuthenticationConfiguration>(options =>
            {
                configuration.GetSection("GoogleAuthenticationConfiguration").Bind(options);
            });

            services.AddTransient<GoogleAuthenticator>();
        });


        var host = hostBuilder.Build();

        var app = host.Services.GetService<App>();

        app.Run();
    }
}
