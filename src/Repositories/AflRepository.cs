using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using afl_dakboard.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace afl_dakboard.Repositories
{
    public class AflRepository
    {
        private const string TeamsCacheKey = "teams";
        private const string StandingsCacheKey = "standings";
        private const string LastGameCacheKey = "lastgame";
        private const string NextGameCacheKey = "nextgame";
        private readonly ILogger<AflRepository> _logger;
        private readonly IMemoryCache _memoryCache;

        public AflRepository(ILogger<AflRepository> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<List<AflTeam>> GetTeams()
        {
            if (_memoryCache.TryGetValue<List<AflTeam>>(TeamsCacheKey, out var teams))
                return teams;

            var url = "https://api.squiggle.com.au/?q=teams";
            _logger.LogInformation("Getting teams from {Url}", url);
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(url);
            teams = JsonConvert.DeserializeObject<AflTeamsRoot>(json).Teams;
            _logger.LogInformation("Found {Count} teams", teams.Count);
            var expiration = DateTime.Now.AddDays(1);
            _logger.LogInformation("Caching data until {Expiration}", expiration);
            _memoryCache.Set(TeamsCacheKey, teams, DateTime.Now.AddDays(1));
            return teams;
        }

        public async Task<List<AflStanding>> GetStandings()
        {
            if (_memoryCache.TryGetValue<List<AflStanding>>(StandingsCacheKey, out var standings))
                return standings;

            var httpClient = new HttpClient();
            var url = "https://api.squiggle.com.au/?q=standings";
            _logger.LogInformation("Getting standings from {Url}", url);
            var json = await httpClient.GetStringAsync(url);
            standings = JsonConvert.DeserializeObject<AflStandingsRoot>(json).Standings;
            _logger.LogInformation("Found {Count} games", standings.Count);
            var expiration = DateTime.Now.AddHours(1);
            _logger.LogInformation("Caching data until {Expiration}", expiration);
            _memoryCache.Set(StandingsCacheKey, standings, expiration);
            return standings;
        }

        public async Task<(AflGame? lastGame, AflGame? nextGame)> GetLastAndNextGamesForRichmond()
        {
            if (_memoryCache.TryGetValue<AflGame>(LastGameCacheKey, out var lastGame) &&
                _memoryCache.TryGetValue<AflGame>(NextGameCacheKey, out var nextGame))
                return (lastGame, nextGame);

            var httpClient = new HttpClient();
            var url = "https://api.squiggle.com.au/?q=games;year=" + DateTime.Now.Year;
            _logger.LogInformation("Getting games for Richmond for this year from {Url}", url);
            var json = await httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<AflGamesRoot>(json);
            var games = response.Games.Where(x => x.AwayTeam == "Richmond" || x.HomeTeam == "Richmond").ToList();
            _logger.LogInformation("Found {Count} games", games.Count);
            lastGame = games.OrderByDescending(x => x.Round).FirstOrDefault(x => x.Complete > 0);
            nextGame = games.OrderBy(x => x.Round).FirstOrDefault(x => x.Complete < 100);

            var expiration = GetGameCacheExpiration(lastGame, nextGame);
            _logger.LogInformation("Caching data until {Expiration}", expiration);
            _memoryCache.Set(LastGameCacheKey, lastGame, expiration);
            _memoryCache.Set(NextGameCacheKey, nextGame, expiration);

            return (lastGame, nextGame);
        }

        private DateTimeOffset GetGameCacheExpiration(AflGame? lastGame, AflGame? nextGame)
        {
            //last game is still in progress
            if (lastGame is {Complete: < 100})
                return DateTime.Now.AddMinutes(10);

            //no next game
            if (nextGame == null)
                return DateTime.Now.AddDays(1);

            //next game has started (not sure if this can happen, but hey)
            if (nextGame.Complete > 0)
                return DateTime.Now.AddMinutes(10);

            //next game is soon
            var timeTillGame = DateTime.Parse(nextGame.Date).Subtract(DateTime.Now);
            //constrain to min 10 min cache, max 6 hour cache
            var timeTillGameTicks = Math.Clamp(timeTillGame.Ticks, TimeSpan.FromMinutes(10).Ticks, TimeSpan.FromHours(6).Ticks);
            return DateTime.Now.AddTicks(timeTillGameTicks);
        }
    }
}
