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
        private const string SeasonsCacheKey = "seasons";
        private const string StandingsCacheKey = "standings";
        private const string LastGameCacheKey = "lastgame";
        private const string NextGameCacheKey = "nextgame";
        private readonly ILogger<CricketRepository> _logger;
        private readonly IMemoryCache _memoryCache;

        public CricketRepository(ILogger<CricketRepository> logger, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _apiToken = configuration["SportMonks:ApiToken"] ?? throw new ApplicationException("SportMonks:ApiToken is required");
        }

        public async Task<IReadOnlyList<CricketTeam>> GetTeams()
        {
            if (_memoryCache.TryGetValue<IReadOnlyList<CricketTeam>>(TeamsCacheKey, out var teams) && teams != null)
            {
                _logger.LogInformation("Cache hit - returning {Collection} from cache", "teams");
                return teams;
            }
            _logger.LogInformation("Cache miss - querying {Collection} from upstream", "teams");

            var url = $"https://cricket.sportmonks.com/api/v2.0/teams?api_token={_apiToken}";
            _logger.LogInformation("Getting teams from {Url}", url.Replace(_apiToken, "xxxxxxxxxx"));
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<CricketTeamsRoot>(json);
            if (response == null)
            {
                _logger.LogWarning("Unable to deserialize response {Json}", json);
                return new List<CricketTeam>();
            }
            teams = response.Teams;
            _logger.LogInformation("Found {Count} teams", teams.Count);
            _memoryCache.Set(TeamsCacheKey, teams, DateTime.Now.AddDays(1));
            return teams;
        }
        
        public async Task<IReadOnlyList<CricketSeason>> GetSeasons(int? leagueId)
        {
            var seasonsCacheKey = SeasonsCacheKey + "_" + leagueId;
            if (_memoryCache.TryGetValue<IReadOnlyList<CricketSeason>>(seasonsCacheKey, out var seasons))
            {
                _logger.LogInformation("Cache hit - returning {Collection} from cache", "seasons");
                return seasons!;
            }
            _logger.LogInformation("Cache miss - querying {Collection} from upstream", "seasons");

            var url = $"https://cricket.sportmonks.com/api/v2.0/seasons?api_token={_apiToken}&filter[league_id]={leagueId}";
            _logger.LogInformation("Getting seasons from {Url}", url.Replace(_apiToken, "xxxxxxxxxx"));
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<CricketSeasonRoot>(json);
            if (response == null)
            {
                _logger.LogWarning("Unable to deserialize response {Json}", json);
                return new List<CricketSeason>();
            }
            seasons = response.Seasons;
            _logger.LogInformation("Found {Count} seasons", seasons.Count);
            _memoryCache.Set(seasonsCacheKey, seasons, DateTime.Now.AddDays(1));
            return seasons;
        }

        public async Task<IReadOnlyList<CricketStanding>> GetStandings(int? seasonId)
        {
            var cacheKey = $"{StandingsCacheKey}_{seasonId}";
            if (_memoryCache.TryGetValue<IReadOnlyList<CricketStanding>>(cacheKey, out var standings) && standings != null)
            {
                _logger.LogInformation("Cache hit - returning {Collection} from cache", "standings");
                return standings;
            }
            _logger.LogInformation("Cache miss - querying {Collection} from upstream", "standings");

            var httpClient = new HttpClient();
            var url = $"https://cricket.sportmonks.com/api/v2.0/standings/season/{seasonId}?api_token={_apiToken}";
            _logger.LogInformation("Getting standings from {Url}", url.Replace(_apiToken, "xxxxxxxxxx"));
            var json = await httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<CricketStandingsRoot>(json);
            if (response == null)
            {
                _logger.LogWarning("Unable to deserialize response {Json}", json);
                return new List<CricketStanding>();
            }
            standings = response.Standings;
            _logger.LogInformation("Found {Count} standings", standings.Count);
            _memoryCache.Set(cacheKey, standings, DateTime.Now.AddHours(1));
            return standings;
        }

        public async Task<(CricketGame? lastGame, CricketGame? nextGame)> GetLastAndNextGamesForTeam(int? seasonId, int teamId)
        {
            var lastGameCacheKey = $"{LastGameCacheKey}_{seasonId}_{teamId}";
            var nextGameCacheKey = $"{NextGameCacheKey}_{seasonId}_{teamId}";
            if (_memoryCache.TryGetValue<CricketGame>(lastGameCacheKey, out var lastGame) &&
                _memoryCache.TryGetValue<CricketGame>(nextGameCacheKey, out var nextGame))
            {
                _logger.LogInformation("Cache hit - returning {Collection} from cache", "last and next game");
                return (lastGame, nextGame);
            }

            //todo: deal with pagination (maybe?)
            _logger.LogInformation("Cache miss - querying {Collection} from upstream", "games");

            var httpClient = new HttpClient();

            var filter = $"filter[season_id]={seasonId}&include=runs,venue,localteam,visitorteam&api_token={_apiToken}";
            var gamesUrl = $"https://cricket.sportmonks.com/api/v2.0/fixtures?page=1&{filter}";

            var games = new List<CricketGame>();
            var totalGames = 0;
            do
            {
                _logger.LogInformation("Getting games from {Url}", gamesUrl.Replace(_apiToken, "xxxxxxxxxx"));
                var gamesJson = await httpClient.GetStringAsync(gamesUrl);
                var gamesResponse = JsonConvert.DeserializeObject<CricketGamesRoot>(gamesJson);
                if (gamesResponse == null)
                {
                    _logger.LogWarning("Unable to deserialize response {Json}", gamesJson);
                    break;
                }
                games.AddRange(gamesResponse.Games);
                gamesUrl = gamesResponse.Links.Next + $"&{filter}";
                totalGames = gamesResponse.Meta.Total;
            } while (totalGames > games.Count);

            _logger.LogInformation("Found {Count} games, {CountWithVenues} of which have venues", 
                games.Count,
                games.Count(x => x.Venue != null));
            var orderedGames = games.OrderBy(x => x.StartingAt).ToArray();
            lastGame = orderedGames.Where(x => x.IsTeam(teamId)).LastOrDefault(x => x.TossWonTeamId != null);
            nextGame = orderedGames.Where(x => x.IsTeam(teamId)).FirstOrDefault(x => x.TossWonTeamId == null);

            if (nextGame == null && lastGame.IsComplete())
            {
                lastGame = orderedGames.LastOrDefault(x => x.TossWonTeamId != null);
                nextGame = orderedGames.FirstOrDefault(x => x.TossWonTeamId == null && !x.IsComplete());
            }
            
            var expiration = GetGameCacheExpiration(lastGame, nextGame);
            _logger.LogInformation("Caching game data until {Expiration}", expiration);
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
                    return nextGame.StartingAt.AddHours(-1);
                return DateTime.Now.AddHours(6);
            }

            if (lastGame.IsInProgress())
                return DateTime.Now.AddMinutes(10);

            //no next game
            if (nextGame == null)
                return DateTime.Now.AddHours(6);
            
            //next game is soon (well, today)
            if (nextGame.StartingAt.ToString("d") == DateTime.Now.ToString("d"))
                return DateTime.Now.AddMinutes(30);

            return DateTime.Now.AddHours(6);
        }
    }
}
