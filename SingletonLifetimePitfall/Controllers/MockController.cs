using SingletonLifetimePitfall.Models;
using SingletonLifetimePitfall.Repositories;

namespace SingletonLifetimePitfall.Controllers;

public class MockController
{
    private readonly IMockRepository _teams;

    public MockController(IMockRepository teams)
    {
        _teams = teams;
    }

    public IEnumerable<MockEntity> Index() => _teams.All();
}
