using ScopedLifetimePitfall.Data;

namespace ScopedLifetimePitfall.Repositories;

public class MockGenericRepository<TKey, TEntity> : IMockGenericRepository<TKey, TEntity> where TEntity : class
{
    private readonly DbSession _session;

    public MockGenericRepository(DbSession session)
    {
        _session = session;
    }

    public ValueTask<TEntity?> Get(TKey id) => _session.Set<TEntity>().FindAsync(id);
}
