using Microsoft.Extensions.DependencyInjection;
using SingletonLifetimePitfall.Processing;

namespace SingletonLifetimePitfall;

internal static class Program
{
    internal static void Main() =>
        new Host(new ServiceCollection())
            .IncomingRequest(new HttpRequest("GET", "/"))
            .IncomingRequest(new HttpRequest("GET", "/foo"))
            .IncomingRequest(new HttpRequest("GET", "/bar"))
            .IncomingRequest(new HttpRequest("GET", "/baz"))
            .Process(RequestProcessing.Parallel);
}
