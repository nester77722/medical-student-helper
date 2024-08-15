using Microsoft.EntityFrameworkCore;

namespace MedicalStudentHelper.API.Entities.Contexts;

public class TestContext : DbContext
{
    public TestContext(DbContextOptions<TestContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Test 1" },
            new User { Id = 2, Name = "Test 2" }
            );
    }
}
