using SingletonLifetimePitfall.Models;

namespace SingletonLifetimePitfall.Repositories;

public interface IMockRepository
{
    MockEntity? Get(int id);
    IEnumerable<MockEntity> All();
}
