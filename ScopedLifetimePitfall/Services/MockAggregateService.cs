using ScopedLifetimePitfall.Models;
using ScopedLifetimePitfall.Repositories;

namespace ScopedLifetimePitfall.Services;

public class MockAggregateService : IMockAggregateService
{
    private readonly IMockGenericRepository<int, FooEntity> _fooEntities;
    private readonly IMockGenericRepository<int, BarEntity> _barEntities;

    public MockAggregateService(IMockGenericRepository<int, FooEntity> fooEntities,
        IMockGenericRepository<int, BarEntity> barEntities)
    {
        _fooEntities = fooEntities;
        _barEntities = barEntities;
    }

    public async Task<(FooEntity?, BarEntity?)> All((int fooId, int barId) ids)
    {
        var task1 = _fooEntities.Get(ids.fooId);
        var task2 = _barEntities.Get(ids.barId);
        await Task.WhenAll(task1.AsTask(), task2.AsTask());
        return (task1.Result, task2.Result);
    }
}
