using MedicalStudentHelper.API.Entities.Contexts;
using MedicalStudentHelper.API.Entities.Contexts.TestContext;
using MedicalStudentHelper.API.Entities.Contexts.UserContext;
using MedicalStudentHelper.API.Services;
using MedicalStudentHelper.API.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace MedicalStudentHelper.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<UserContextConfiguration>(
            builder.Configuration.GetSection(nameof(UserContextConfiguration)));

        builder.Services.AddTransient(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<UserContextConfiguration>>().Value;
            return new UserContext(settings);
        });

        builder.Services.Configure<TestContextConfiguration>(
            builder.Configuration.GetSection(nameof(TestContextConfiguration)));

        builder.Services.AddTransient(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<TestContextConfiguration>>().Value;
            return new TestContext(settings);
        });

        builder.Services.AddTransient<ITestService, TestService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect("/swagger");
                return;
            }

            await next();
        });

        app.MapControllers();

        app.Run();
    }
}
