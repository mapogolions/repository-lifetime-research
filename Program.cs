using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RepositoryLifetimeResearch;

internal static class Program
{
    internal static void Main()
    {
        var services = new ServiceCollection();
        Console.WriteLine("hello");
    }
}

public class DbSession : DbContext
{
    private readonly IConfiguration _config;

    public DbSession(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnectionString"));
    }
}
