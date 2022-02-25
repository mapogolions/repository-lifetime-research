using SingletonLifetimePitfall.Data;
using SingletonLifetimePitfall.Models;

namespace SingletonLifetimePitfall.Repositories;

public class MockRepository : IMockRepository
{
    private readonly DbSession _session;

    public MockRepository(DbSession session)
    {
        _session = session;
    }

    public IEnumerable<MockEntity> All() => _session.Set<MockEntity>().ToList();

    public MockEntity? Get(int id) => _session.Set<MockEntity>().FirstOrDefault(x => x.Id == id);
}
