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
    public class AflController : Controller
    {
        private readonly ILogger<AflController> _logger;
        private readonly AflRepository _repository;

        public AflController(ILogger<AflController> logger, AflRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Ladder()
        {
            var teams = await _repository.GetTeams();
            var standings = await _repository.GetStandings();
            _logger.LogInformation("Standings are {Standings}", JsonConvert.SerializeObject(standings));

            return View(new AflLadderViewModel(teams, standings));
        }

        public async Task<IActionResult> Richmond()
        {
            var (lastGame, nextGame) = await _repository.GetLastAndNextGamesForRichmond();

            _logger.LogInformation("Last game is {Game}", JsonConvert.SerializeObject(lastGame));
            _logger.LogInformation("Next game is {Game}", JsonConvert.SerializeObject(nextGame));

            return View(new RichmondViewModel(lastGame, nextGame, _logger));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel(Activity.Current?.Id ?? HttpContext.TraceIdentifier));
        }
    }
}
