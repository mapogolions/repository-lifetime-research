using SingletonLifetimePitfall.Data;
using SingletonLifetimePitfall.Models;

namespace SingletonLifetimePitfall.Repositories;

public class TeamsRepository : ITeamsRepository
{
    private readonly DbSession _session;

    public TeamsRepository(DbSession session)
    {
        _session = session;
    }

    public IEnumerable<Team> All() => _session.Set<Team>().ToList();

    public Team? Get(int id) => _session.Set<Team>().FirstOrDefault(x => x.Id == id);
}
