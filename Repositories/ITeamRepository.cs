using RepositoryLifetimeResearch.Models;

namespace RepositoryLifetimeResearch.Repositories;

public interface ITeamRepository
{
    Team? Get(int id);
    IEnumerable<Team> All();
}
