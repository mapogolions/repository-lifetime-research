using Microsoft.Extensions.DependencyInjection;
using SingletonLifetimePitfall.Processing;

namespace SingletonLifetimePitfall;

internal static class Program
{
    internal static void Main() =>
        new Host(new ServiceCollection())
            .IncomingRequest(new MockRequest("GET", "/"))
            .IncomingRequest(new MockRequest("GET", "/foo"))
            .IncomingRequest(new MockRequest("GET", "/bar"))
            .IncomingRequest(new MockRequest("GET", "/baz"))
            .Process(RequestProcessing.Parallel)
            // .Process(RequestProcessing.Sequential)
            ;
}
