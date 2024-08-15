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

        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        //builder.Services.AddDbContext<TestContext>(options =>
        //{
        //    options.UseSqlite(connectionString);
        //});

        var app = builder.Build();

        //using (var scope = app.Services.CreateScope())
        //{
        //    var dbContext = scope.ServiceProvider.GetRequiredService<TestContext>();
        //    dbContext.Database.Migrate(); // Это применит любые существующие миграции
        //}

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
