using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SingletonLifetimePitfall.Controllers;
using SingletonLifetimePitfall.Data;
using SingletonLifetimePitfall.Processing;
using SingletonLifetimePitfall.Repositories;

namespace SingletonLifetimePitfall;

public class Host
{
    private readonly List<MockRequest> _incomingRequests = new();
    private readonly Lazy<IServiceCollection> _services;

    public Host(IServiceCollection services)
    {
        _services = new(() => ConfigureServices(services));
    }

    public Host IncomingRequest(MockRequest request)
    {
        _incomingRequests.Add(request);
        return this;
    }

    public void Process(RequestProcessing requestProcessing)
    {
        using (var serviceProvider = _services.Value.BuildServiceProvider())
        {
            IRequestProcessing processing = requestProcessing switch
                {
                    RequestProcessing.Parallel => new ParallelProcessing(serviceProvider),
                    RequestProcessing.Sequential => new SequentialProcessing(serviceProvider),
                    _ => throw new NotImplementedException()
                };
            var session = serviceProvider.GetRequiredService<DbSession>();
            session.Database.EnsureCreated();
            processing.Process(_incomingRequests);
            session.Database.EnsureDeleted();
        }
    }

    private static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IConfiguration>(x =>
            new ConfigurationBuilder()
                .AddIniFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appSettings.ini"))
                .Build());
        services.AddDbContext<DbSession>();
        // services.AddSingleton<IMockRepository, MockRepository>(); // uncomment to reproduce issue
        services.AddScoped<IMockRepository, MockRepository>(); // comment to repoduce issue
        services.AddScoped<MockController>();
        return services;
    }
}
