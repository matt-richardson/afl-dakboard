using System.Diagnostics;
using System.Threading.Tasks;
using afl_dakboard.Models;
using afl_dakboard.Repositories;
using afl_dakboard.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace afl_dakboard.Controllers
{
    public class BigBashController : Controller
    {
        private readonly ILogger<BigBashController> _logger;
        private readonly BigBashRepository _repository;

        public BigBashController(ILogger<BigBashController> logger, BigBashRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Ladder()
        {
            var teams = await _repository.GetTeams();
            var standings = await _repository.GetStandings();
            _logger.LogInformation("Standings are {Standings}", JsonConvert.SerializeObject(standings));

            return View(new BigBashLadderViewModel(teams, standings));
        }

        public async Task<IActionResult> MelbourneStars()
        {
            var (lastGame, nextGame) = await _repository.GetLastAndNextGamesForMelbourneStars();

            _logger.LogInformation("Last game is {@Game}", lastGame);
            _logger.LogInformation("Next game is {@Game}", nextGame);

            return View(new CricketViewModel(lastGame, nextGame, _logger, _repository.TeamId));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel(Activity.Current?.Id ?? HttpContext.TraceIdentifier));
        }
    }
}
