using Microsoft.Extensions.DependencyInjection;
using SingletonLifetimePitfall.Controllers;

namespace SingletonLifetimePitfall.Processing;

public class SequentialProcessing : IRequestProcessing
{
    private readonly IServiceProvider _serviceProvider;

    public SequentialProcessing(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Process(IEnumerable<MockRequest> requests)
    {
        var task = requests.Aggregate(Task.CompletedTask, (task, request) =>
            task.ContinueWith(completedTask =>
                {
                    using (var requestScope = _serviceProvider.CreateScope())
                    {
                        var teamsController = requestScope.ServiceProvider.GetRequiredService<MockController>();
                        _ = teamsController.Index();
                        Console.WriteLine($"{request} done");
                    }
                }));
        task.Wait();
    }
}
