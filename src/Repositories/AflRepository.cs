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
        private const string userAgent = "afl-dakboard/1.0 github.com / matt-richardson";

        public AflRepository(ILogger<AflRepository> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<List<AflTeam>> GetTeams()
        {
            if (_memoryCache.TryGetValue<List<AflTeam>>(TeamsCacheKey, out var teams) && teams != null)
                return teams;

            var url = "https://api.squiggle.com.au/?q=teams";
            _logger.LogInformation("Getting teams from {Url}", url);
            var json = await GetString(url);
            var response = JsonConvert.DeserializeObject<AflTeamsRoot>(json);
            if (response == null)
            {
                _logger.LogWarning("Unable to deserialize response {Json}", json);
                return new List<AflTeam>();
            }
            teams = response.Teams;
            _logger.LogInformation("Found {Count} teams", teams.Count);
            var expiration = DateTime.Now.AddDays(1);
            _logger.LogInformation("Caching data until {Expiration}", expiration);
            _memoryCache.Set(TeamsCacheKey, teams, DateTime.Now.AddDays(1));
            return teams;
        }

        private static async Task<string> GetString(string url)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            var json = await httpClient.GetStringAsync(url);
            if (json.Contains("\"warning\""))
                throw new Exception(json);
            return json;
        }

        public async Task<List<AflStanding>> GetStandings()
        {
            if (_memoryCache.TryGetValue<List<AflStanding>>(StandingsCacheKey, out var standings) && standings != null)
                return standings;

            var url = "https://api.squiggle.com.au/?q=standings";
            var json = await GetString(url);
            var response = JsonConvert.DeserializeObject<AflStandingsRoot>(json);
            if (response == null)
            {
                _logger.LogWarning("Unable to deserialize response {Json}", json);
                return new List<AflStanding>();
            }
            standings = response.Standings;
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

            var url = "https://api.squiggle.com.au/?q=games;year=" + DateTime.Now.Year;
            var json = await GetString(url);
            var response = JsonConvert.DeserializeObject<AflGamesRoot>(json);
            if (response == null)
            {
                _logger.LogWarning("Unable to deserialize response {Json}", json);
                return (null, null);
            }
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
