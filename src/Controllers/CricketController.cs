using System.Diagnostics;
using System.Threading.Tasks;
using afl_dakboard.Models;
using afl_dakboard.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace afl_dakboard.Controllers
{
    public class CricketController : Controller
    {
        private readonly ILogger<CricketController> _logger;
        private readonly CricketRepository _repository;

        public CricketController(ILogger<CricketController> logger, CricketRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Ladder()
        {
            var teams = await _repository.GetTeams();
            var standings = await _repository.GetStandings();
            _logger.LogInformation("Standings are {Standings}", JsonConvert.SerializeObject(standings));

            return View(new CricketLadderViewModel(teams, standings));
        }

        public async Task<IActionResult> MelbourneStars()
        {
            var (lastGame, nextGame) = await _repository.GetLastAndNextGamesForMelbourneStars();

            _logger.LogInformation("Last game is {Game}", JsonConvert.SerializeObject(lastGame));
            _logger.LogInformation("Next game is {Game}", JsonConvert.SerializeObject(nextGame));

            return View(new MelbourneStarsViewModel(lastGame, nextGame, _logger));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel(Activity.Current?.Id ?? HttpContext.TraceIdentifier));
        }
    }
}
