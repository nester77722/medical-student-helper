using MedicalStudentHelper.API.Entities.Contexts;
using Microsoft.EntityFrameworkCore;

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

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        var databasePath = Path.GetDirectoryName(connectionString.Split('=')[1]);

        // ѕроверка и создание папки дл€ базы данных
        if (!Directory.Exists(databasePath))
        {
            Directory.CreateDirectory(databasePath);
            Console.WriteLine($"Created directory: {databasePath}");
        }

        // —оздание пустого файла базы данных, если он не существует
        var databaseFile = connectionString.Split('=')[1];
        if (!File.Exists(databaseFile))
        {
            File.Create(databaseFile).Dispose();
            Console.WriteLine($"Created database file: {databaseFile}");
        }

        builder.Services.AddDbContext<TestContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TestContext>();
            dbContext.Database.Migrate(); // Ёто применит любые существующие миграции
        }

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
