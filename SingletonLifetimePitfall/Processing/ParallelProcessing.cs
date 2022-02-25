using Microsoft.Extensions.DependencyInjection;
using SingletonLifetimePitfall.Controllers;

namespace SingletonLifetimePitfall.Processing;

public class ParallelProcessing : IRequestProcessing
{
    private readonly IServiceProvider _serviceProvider;

    public ParallelProcessing(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Process(IEnumerable<MockRequest> requests)
    {
        var tasks = requests.Select(request =>
            new Task(() =>
            {
                using (var requestScope = _serviceProvider.CreateScope())
                {
                    var controller = requestScope.ServiceProvider.GetRequiredService<MockController>();
                    _ = controller.Index();
                    Console.WriteLine($"{request} done");
                }
            }))
            .ToArray();
        foreach (var task in tasks) task.Start();
        Task.WaitAll(tasks);
    }
}
