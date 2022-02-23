using RepositoryLifetimeResearch.Models;

namespace RepositoryLifetimeResearch.Repositories;

public interface ITeamsRepository
{
    Team? Get(int id);
    IEnumerable<Team> All();
}
