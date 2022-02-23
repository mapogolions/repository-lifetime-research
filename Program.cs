using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SingletonLifetimePItfall.Controllers;
using SingletonLifetimePItfall.Data;
using SingletonLifetimePItfall.Repositories;

namespace SingletonLifetimePItfall;

internal static class Program
{
    internal static void Main()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        using (var serviceProvider = services.BuildServiceProvider())
        {
            var dbSession = serviceProvider.GetRequiredService<DbSession>();
            dbSession.Database.EnsureCreated();
            var requests = Enumerable.Range(1, 8).Select(x =>
                new Task(() =>
                {
                    using (var requestScope = serviceProvider.CreateScope())
                    {
                        var teamsController = requestScope.ServiceProvider.GetRequiredService<TeamsController>();
                        _ = teamsController.Index();
                        Console.WriteLine($"Request {x} done");
                    }
                }))
                .ToArray();
            foreach (var request in requests) request.Start();
            Task.WaitAll(requests);
            dbSession.Database.EnsureDeleted();
        }
    }

    internal static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IConfiguration>(x =>
            new ConfigurationBuilder()
                .AddIniFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appSettings.ini"))
                .Build());
        services.AddDbContext<DbSession>();
        // services.AddSingleton<ITeamsRepository, TeamsRepository>(); // uncomment to reproduce issue
        services.AddScoped<ITeamsRepository, TeamsRepository>(); // comment to repoduce issue
        services.AddScoped<TeamsController>();
    }
}
