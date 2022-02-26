namespace ScopedLifetimePitfall.Repositories;

public interface IMockGenericRepository<TKey, TEntity> where TEntity : class
{
    ValueTask<TEntity?> Get(TKey id);
}
