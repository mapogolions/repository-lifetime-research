using SingletonLifetimePItfall.Models;

namespace SingletonLifetimePItfall.Repositories;

public interface ITeamsRepository
{
    Team? Get(int id);
    IEnumerable<Team> All();
}
