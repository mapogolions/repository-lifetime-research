using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SingletonLifetimePItfall.Models;

namespace SingletonLifetimePItfall.Data;

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
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
