using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using afl_dakboard.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace afl_dakboard.Controllers
{
    public class Repository
    {
        private readonly ILogger<Repository> _logger;

        public Repository(ILogger<Repository> logger)
        {
            _logger = logger;
        }

        public async Task<List<Team>> GetTeams()
        {
            var url = "https://api.squiggle.com.au/?q=teams";
            _logger.LogInformation("Getting teams from {Url}", url);
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<TeamsRoot>(json);
            _logger.LogInformation("Found {Count} teams", response.teams.Count);
            return response.teams;
        }

        public async Task<List<Standing>> GetStandings()
        {
            var httpClient = new HttpClient();
            var url = "https://api.squiggle.com.au/?q=standings";
            _logger.LogInformation("Getting standings from {Url}", url);
            var json = await httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<StandingsRoot>(json);
            _logger.LogInformation("Found {Count} games", response.standings.Count);
            return response.standings;
        }

        public async Task<List<Game>> GetGamesForRichmondForThisYear()
        {
            var httpClient = new HttpClient();
            var url = "https://api.squiggle.com.au/?q=games;year=" + DateTime.Now.Year;
            _logger.LogInformation("Getting games for Richmond for this year from {Url}", url);
            var json = await httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<GamesRoot>(json);
            var games = response.games.Where(x => x.ateam == "Richmond" || x.hteam == "Richmond").ToList();
            _logger.LogInformation("Found {Count} games", games.Count);
            return games;
        }
    }
}
