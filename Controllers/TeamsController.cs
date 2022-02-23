using SingletonLifetimePitfall.Models;
using SingletonLifetimePitfall.Repositories;

namespace SingletonLifetimePitfall.Controllers;

public class TeamsController
{
    private readonly ITeamsRepository _teams;

    public TeamsController(ITeamsRepository teams)
    {
        _teams = teams;
    }

    public IEnumerable<Team> Index() => _teams.All();
}
