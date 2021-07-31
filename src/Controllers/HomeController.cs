using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using afl_dakboard.Models;
using afl_dakboard.ViewModels;
using Newtonsoft.Json;

namespace afl_dakboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Repository _repository;

        public HomeController(ILogger<HomeController> logger, Repository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _repository.GetTeams();
            var standings = await _repository.GetStandings();

            return View(new IndexViewModel(teams, standings));
        }

        public async Task<IActionResult> Richmond()
        {
            var games = await (_repository.GetGamesForRichmondForThisYear());

            var lastGame = games.OrderByDescending(x => x.round).First(x => x.complete > 0);
            var nextGame = games.OrderBy(x => x.round).FirstOrDefault(x => x.complete < 100);
            _logger.LogInformation("Last game is {Game}", JsonConvert.SerializeObject(lastGame));
            _logger.LogInformation("Next game is {Game}", JsonConvert.SerializeObject(nextGame));
            return View(new RichmondViewModel(lastGame, nextGame));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
