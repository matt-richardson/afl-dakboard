using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using afl_dakboard.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace afl_dakboard.Repositories
{
    public class CricketRepository
    {
        private readonly string _apiToken;

        private const string TeamsCacheKey = "teams";
        private const string StandingsCacheKey = "standings";
        private const string LastGameCacheKey = "lastgame";
        private const string NextGameCacheKey = "nextgame";
        private readonly ILogger<CricketRepository> _logger;
        private readonly IMemoryCache _memoryCache;

        public CricketRepository(ILogger<CricketRepository> logger, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _apiToken = configuration["SportMonks:ApiToken"];
        }

        public async Task<IReadOnlyList<CricketTeam>> GetTeams()
        {
            if (_memoryCache.TryGetValue<IReadOnlyList<CricketTeam>>(TeamsCacheKey, out var teams))
            {
                _logger.LogInformation("Cache hit - returning teams from cache");
                return teams;
            }
            _logger.LogInformation("Cache miss - querying teams from upstream");

            var url = $"https://cricket.sportmonks.com/api/v2.0/teams?api_token={_apiToken}";
            _logger.LogInformation("Getting teams from {Url}", url.Replace(_apiToken, "xxxxxxxxxx"));
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(url);
            teams = JsonConvert.DeserializeObject<CricketTeamsRoot>(json).Teams;
            _logger.LogInformation("Found {Count} teams", teams.Count);
            _memoryCache.Set(TeamsCacheKey, teams, DateTime.Now.AddDays(1));
            return teams;
        }

        public async Task<IReadOnlyList<CricketStanding>> GetStandings(int seasonId)
        {
            var cacheKey = $"{StandingsCacheKey}_{seasonId}";
            if (_memoryCache.TryGetValue<IReadOnlyList<CricketStanding>>(cacheKey, out var standings))
            {
                _logger.LogInformation("Cache hit - returning standings from cache");
                return standings;
            }
            _logger.LogInformation("Cache miss - querying standings from upstream");

            var httpClient = new HttpClient();
            var url = $"https://cricket.sportmonks.com/api/v2.0/standings/season/{seasonId}?api_token={_apiToken}";
            _logger.LogInformation("Getting standings from {Url}", url.Replace(_apiToken, "xxxxxxxxxx"));
            var json = await httpClient.GetStringAsync(url);
            standings = JsonConvert.DeserializeObject<CricketStandingsRoot>(json).Standings;
            _logger.LogInformation("Found {Count} standings", standings.Count);
            _memoryCache.Set(cacheKey, standings, DateTime.Now.AddHours(1));
            return standings;
        }

        public async Task<(CricketGame? lastGame, CricketGame? nextGame)> GetLastAndNextGamesForTeam(int seasonId, int teamId)
        {
            var lastGameCacheKey = $"{LastGameCacheKey}_{seasonId}_{teamId}";
            var nextGameCacheKey = $"{NextGameCacheKey}_{seasonId}_{teamId}";
            if (_memoryCache.TryGetValue<CricketGame>(lastGameCacheKey, out var lastGame) &&
                _memoryCache.TryGetValue<CricketGame>(nextGameCacheKey, out var nextGame))
            {
                _logger.LogInformation("Cache hit - returning last and next game from cache");
                return (lastGame, nextGame);
            }

            //todo: deal with pagination (maybe?)
            _logger.LogInformation("Cache miss - querying games from upstream");

            var httpClient = new HttpClient();

            var localGamesUrl = $"https://cricket.sportmonks.com/api/v2.0/fixtures?filter[season_id]={seasonId}&include=runs,venue,localteam,visitorteam&filter[localteam_id]={teamId}&api_token={_apiToken}";
            _logger.LogInformation("Getting games from {Url}", localGamesUrl.Replace(_apiToken, "xxxxxxxxxx"));
            var localGamesJson = await httpClient.GetStringAsync(localGamesUrl);
            var localGamesResponse = JsonConvert.DeserializeObject<CricketGamesRoot>(localGamesJson);

            if (localGamesResponse.Meta.Total > localGamesResponse.Meta.PerPage)
                throw new ApplicationException("We got more games back than we currently handle. Need to deal with pagination.");

            var awayGamesUrl = $"https://cricket.sportmonks.com/api/v2.0/fixtures?filter[season_id]={seasonId}&include=runs,venue,localteam,visitorteam&filter[visitorteam_id]={teamId}&api_token={_apiToken}";
            _logger.LogInformation("Getting games for from {Url}", awayGamesUrl.Replace(_apiToken, "xxxxxxxxxx"));
            var awayGamesJson = await httpClient.GetStringAsync(awayGamesUrl);
            var awayGamesResponse = JsonConvert.DeserializeObject<CricketGamesRoot>(awayGamesJson);

            if (awayGamesResponse.Meta.Total > awayGamesResponse.Meta.PerPage)
                throw new ApplicationException("We got more games back than we currently handle. Need to deal with pagination.");

            var games = localGamesResponse.Games.Concat(awayGamesResponse.Games).ToList();

            _logger.LogInformation("Found {Count} games", games.Count);
            var orderedGames = games.OrderBy(x => x.StartingAt).ToArray();
            lastGame = orderedGames.LastOrDefault(x => x.TossWonTeamId != null);
            nextGame = orderedGames.FirstOrDefault(x => x.TossWonTeamId == null);

            var expiration = GetGameCacheExpiration(lastGame, nextGame);
            _logger.LogInformation("Caching data until {Expiration}", expiration);
            _memoryCache.Set(lastGameCacheKey, lastGame, expiration);
            _memoryCache.Set(nextGameCacheKey, nextGame, expiration);

            return (lastGame, nextGame);
        }

        private DateTimeOffset GetGameCacheExpiration(CricketGame? lastGame, CricketGame? nextGame)
        {
            //last game has finished
            if (lastGame.IsComplete())
            {
                if (nextGame != null && nextGame.StartingAt < DateTime.Now.AddDays(1))
                    return nextGame.StartingAt;
                return DateTime.Now.AddDays(1);
            }

            //no next game
            if (nextGame == null)
                return DateTime.Now.AddDays(1);

            if (lastGame.IsInProgress())
                return DateTime.Now.AddMinutes(10);

            //next game is soon (well, today)
            if (nextGame.StartingAt.ToString("d") == DateTime.Now.ToString("d"))
                return DateTime.Now.AddMinutes(30);

            return DateTime.Now.AddHours(12);
        }
    }
}
