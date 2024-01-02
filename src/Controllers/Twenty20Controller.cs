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
    public class Twenty20Controller : Controller
    {
        private readonly ILogger<Twenty20Controller> _logger;
        private readonly Twenty20Repository _repository;

        public Twenty20Controller(ILogger<Twenty20Controller> logger, Twenty20Repository repository)
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

        public async Task<IActionResult> Australia()
        {
            var (lastGame, nextGame) = await _repository.GetLastAndNextGamesForAustralia();

            _logger.LogInformation("Last game is {Game}", JsonConvert.SerializeObject(lastGame));
            _logger.LogInformation("Next game is {Game}", JsonConvert.SerializeObject(nextGame));

            return View(new CricketViewModel(lastGame, nextGame, _logger, _repository.TeamId));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel(Activity.Current?.Id ?? HttpContext.TraceIdentifier));
        }
    }
}
