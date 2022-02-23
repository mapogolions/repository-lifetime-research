using Microsoft.Extensions.DependencyInjection;
using SingletonLifetimePitfall.Controllers;
using SingletonLifetimePitfall.Data;

namespace SingletonLifetimePitfall.Processing;

public class SequentialProcessing : IRequestProcessing
{
    private readonly IServiceCollection _services;

    public SequentialProcessing(IServiceCollection services)
    {
        _services = services;
    }

    public void Process(IEnumerable<HttpRequest> requests)
    {
         using (var serviceProvider = _services.BuildServiceProvider())
        {
            var session = serviceProvider.GetRequiredService<DbSession>();
            session.Database.EnsureCreated();
            var task = requests.Aggregate(Task.CompletedTask, (task, request) =>
                task.ContinueWith(completedTask =>
                    {
                        using (var requestScope = serviceProvider.CreateScope())
                        {
                            var teamsController = requestScope.ServiceProvider.GetRequiredService<TeamsController>();
                            _ = teamsController.Index();
                            Console.WriteLine($"{request} done");
                        }
                    }));

            task.Wait();
            session.Database.EnsureDeleted();
        }
    }
}
