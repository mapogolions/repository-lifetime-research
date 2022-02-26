using Microsoft.Extensions.DependencyInjection;
using ScopedLifetimePitfall.Processing;

namespace ScopedLifetimePitfall;

internal static class Program
{
    internal static void Main() =>
        new Host(new ServiceCollection())
            .IncomingRequest(new MockRequest("GET", "/"))
            .Process(RequestProcessing.Sequential)
            ;
}
