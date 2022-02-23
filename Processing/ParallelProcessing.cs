using Microsoft.Extensions.DependencyInjection;
using SingletonLifetimePitfall.Controllers;
using SingletonLifetimePitfall.Data;

namespace SingletonLifetimePitfall.Processing;

public class ParallelProcessing : IRequestProcessing
{
    private readonly IServiceCollection _services;

    public ParallelProcessing(IServiceCollection services)
    {
        _services = services;
    }

    public void Process(IEnumerable<HttpRequest> requests)
    {
         using (var serviceProvider = _services.BuildServiceProvider())
        {
            var session = serviceProvider.GetRequiredService<DbSession>();
            session.Database.EnsureCreated();
            var tasks = requests.Select(request =>
                new Task(() =>
                {
                    using (var requestScope = serviceProvider.CreateScope())
                    {
                        var teamsController = requestScope.ServiceProvider.GetRequiredService<TeamsController>();
                        _ = teamsController.Index();
                        Console.WriteLine($"{request} done");
                    }
                }))
                .ToArray();
            foreach (var task in tasks) task.Start();
            Task.WaitAll(tasks);
            session.Database.EnsureDeleted();
        }
    }
}
