using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SingletonLifetimePitfall.Controllers;
using SingletonLifetimePitfall.Data;
using SingletonLifetimePitfall.Processing;
using SingletonLifetimePitfall.Repositories;

namespace SingletonLifetimePitfall;

public class Host
{
    private readonly List<HttpRequest> _incomingRequests = new();
    private readonly Lazy<IServiceCollection> _services;

    public Host(IServiceCollection services)
    {
        _services = new(() => ConfigureServices(services));
    }

    public Host IncomingRequest(HttpRequest request)
    {
        _incomingRequests.Add(request);
        return this;
    }

    public void Process(RequestProcessing requestProcessing)
    {
        switch (requestProcessing)
        {
            case RequestProcessing.Parallel:
                new ParallelProcessing(_services.Value).Process(_incomingRequests);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IConfiguration>(x =>
            new ConfigurationBuilder()
                .AddIniFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appSettings.ini"))
                .Build());
        services.AddDbContext<DbSession>();
        // services.AddSingleton<ITeamsRepository, TeamsRepository>(); // uncomment to reproduce issue
        services.AddScoped<ITeamsRepository, TeamsRepository>(); // comment to repoduce issue
        services.AddScoped<TeamsController>();
        return services;
    }
}
