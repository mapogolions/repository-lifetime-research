using ScopedLifetimePitfall.Models;

namespace ScopedLifetimePitfall.Services;

public interface IMockAggregateService
{
    Task<(FooEntity?, BarEntity?)> All((int fooId, int barId) ids);
}
