using SingletonLifetimePItfall.Models;
using SingletonLifetimePItfall.Repositories;

namespace SingletonLifetimePItfall.Controllers;

public class TeamsController
{
    private readonly ITeamsRepository _teams;

    public TeamsController(ITeamsRepository teams)
    {
        _teams = teams;
    }

    public IEnumerable<Team> Index() => _teams.All();
}
