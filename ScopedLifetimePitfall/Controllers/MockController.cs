using ScopedLifetimePitfall.Models;
using ScopedLifetimePitfall.Services;

namespace ScopedLifetimePitfall.Controllers;

public class MockController
{
    private readonly IMockAggregateService _service;

    public MockController(IMockAggregateService service)
    {
        _service = service;
    }

    public Task<(FooEntity?, BarEntity?)> Index() => _service.All((1, 2));
}
