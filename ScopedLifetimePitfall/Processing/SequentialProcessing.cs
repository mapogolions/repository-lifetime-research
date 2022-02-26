using Microsoft.Extensions.DependencyInjection;
using ScopedLifetimePitfall.Controllers;

namespace ScopedLifetimePitfall.Processing;

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
                        var controller = requestScope.ServiceProvider.GetRequiredService<MockController>();
                        controller.Index().Wait();
                        Console.WriteLine($"{request} done");
                    }
                }));
        task.Wait();
    }
}
