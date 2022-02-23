using SingletonLifetimePitfall.Models;

namespace SingletonLifetimePitfall.Repositories;

public interface ITeamsRepository
{
    Team? Get(int id);
    IEnumerable<Team> All();
}
