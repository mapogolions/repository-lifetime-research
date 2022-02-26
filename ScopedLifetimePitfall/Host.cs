using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScopedLifetimePitfall.Controllers;
using ScopedLifetimePitfall.Data;
using ScopedLifetimePitfall.Processing;
using ScopedLifetimePitfall.Repositories;
using ScopedLifetimePitfall.Services;

namespace ScopedLifetimePitfall;

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
        // services.AddDbContext<DbSession>(); // uncomment to reproduce issue
        services.AddTransient<DbSession>(); // comment to reproduce issue
        services.AddScoped(typeof(IMockGenericRepository<,>), typeof(MockGenericRepository<,>));
        services.AddScoped<IMockAggregateService, MockAggregateService>();
        services.AddScoped<MockController>();
        return services;
    }
}
