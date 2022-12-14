using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afl_dakboard.Models;

namespace afl_dakboard.Repositories
{
    public class BigBashRepository
    {
        private const int BigBashLeagueId = 5;
        private const int BigBashMelbourneStarsTeamId = 51;

        private readonly CricketRepository _cricketRepository;

        public BigBashRepository(CricketRepository cricketRepository)
        {
            _cricketRepository = cricketRepository;
        }

        public int TeamId => BigBashMelbourneStarsTeamId;

        async Task<int?> GetSeasonId()
        {
            var seasons = await _cricketRepository.GetSeasons(BigBashLeagueId);

            var seasonName = DateTime.Now.Month > 10 
                ? $"{DateTime.Now.Year}/{DateTime.Now.Year + 1}" 
                : $"{DateTime.Now.Year - 1}/{DateTime.Now.Year}";
            return seasons.FirstOrDefault(x => x.Name == seasonName)?.Id;
        }

        public async Task<IReadOnlyList<CricketTeam>> GetTeams()
            => await _cricketRepository.GetTeams();

        public async Task<IReadOnlyList<CricketStanding>> GetStandings()
            => await _cricketRepository.GetStandings(await GetSeasonId());

        public async Task<(CricketGame? lastGame, CricketGame? nextGame)> GetLastAndNextGamesForMelbourneStars()
            => await _cricketRepository.GetLastAndNextGamesForTeam(await GetSeasonId(), BigBashMelbourneStarsTeamId);
    }
}
