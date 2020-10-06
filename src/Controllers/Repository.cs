using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using afl_dakboard.Models;
using Newtonsoft.Json;

namespace afl_dakboard.Controllers
{
    public class Repository
    {
        public async Task<List<Team>> GetTeams()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://api.squiggle.com.au/?q=teams");
            var response = JsonConvert.DeserializeObject<TeamsRoot>(json);
            return response.teams;
        }

        public async Task<List<Standing>> GetStandings()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://api.squiggle.com.au/?q=standings");
            var response = JsonConvert.DeserializeObject<StandingsRoot>(json);
            return response.standings;
        }

        public async Task<List<Game>> GetGamesForRichmondForThisYear()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://api.squiggle.com.au/?q=games;year=" + DateTime.Now.Year);
            var response = JsonConvert.DeserializeObject<GamesRoot>(json);
            return response.games.Where(x => x.ateam == "Richmond" || x.hteam == "Richmond").ToList();
        }
    }
}
