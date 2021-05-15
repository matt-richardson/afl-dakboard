using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using afl_dakboard.Models;

namespace afl_dakboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Repository _repository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _repository = new Repository();
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

            return View(new RichmondViewModel(games));
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
