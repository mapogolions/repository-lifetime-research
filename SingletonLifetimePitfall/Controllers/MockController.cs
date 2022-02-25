using SingletonLifetimePitfall.Models;
using SingletonLifetimePitfall.Repositories;

namespace SingletonLifetimePitfall.Controllers;

public class MockController
{
    private readonly IMockRepository _entities;

    public MockController(IMockRepository entities)
    {
        _entities = entities;
    }

    public IEnumerable<MockEntity> Index() => _entities.All();
}
