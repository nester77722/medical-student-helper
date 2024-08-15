using MedicalStudentHelper.API.Entities.Contexts;
using MedicalStudentHelper.API.Entities.Contexts.UserContex;
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

        var connectionString = builder.Configuration.GetConnectionString("MongoAtlasConnection");

        builder.Services.Configure<UserContextConfiguration>(
            builder.Configuration.GetSection(nameof(UserContextConfiguration)));

        builder.Services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<UserContextConfiguration>>().Value;
            return new UserContext(settings);
        });

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
