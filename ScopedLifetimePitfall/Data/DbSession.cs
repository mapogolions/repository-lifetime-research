using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ScopedLifetimePitfall.Models;

namespace ScopedLifetimePitfall.Data;

public class DbSession : DbContext
{
    private readonly IConfiguration _config;

    public DbSession(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FooEntity>();
        modelBuilder.Entity<BarEntity>();
        base.OnModelCreating(modelBuilder);
    }
}
