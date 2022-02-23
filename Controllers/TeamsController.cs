using RepositoryLifetimeResearch.Models;
using RepositoryLifetimeResearch.Repositories;

namespace RepositoryLifetimeResearch.Controllers;

public class TeamsController
{
    private readonly ITeamsRepository _teams;

    public TeamsController(ITeamsRepository teams)
    {
        _teams = teams;
    }

    public IEnumerable<Team> Index() => _teams.All();
}
