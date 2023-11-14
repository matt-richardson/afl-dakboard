using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afl_dakboard.Models;

namespace afl_dakboard.Repositories
{
    public class Twenty20Repository
    {
        private const int Twenty20InternationalLeagueId = 3;
        private const int AustralianTeamId = 36;

        private readonly CricketRepository _cricketRepository;

        public Twenty20Repository(CricketRepository cricketRepository)
        {
            _cricketRepository = cricketRepository;
        }

        public int TeamId => AustralianTeamId;

        async Task<int?> GetSeasonId()
        {
            var seasons = await _cricketRepository.GetSeasons(Twenty20InternationalLeagueId);

            string combinedSeasonName;
            var singleSeasonName = $"{DateTime.Now.Year}";
            if (DateTime.Now.Month > 10)
            {
                combinedSeasonName = $"{DateTime.Now.Year}/{DateTime.Now.Year + 1}";
            }
            else
            {
                combinedSeasonName = $"{DateTime.Now.Year - 1}/{DateTime.Now.Year}";
            }
            return seasons.FirstOrDefault(x => x.Name == combinedSeasonName || x.Name == singleSeasonName)?.Id;
        }
        
        public async Task<IReadOnlyList<CricketTeam>> GetTeams()
            => await _cricketRepository.GetTeams();

        public async Task<IReadOnlyList<CricketStanding>> GetStandings()
            => await _cricketRepository.GetStandings(await GetSeasonId());

        public async Task<(CricketGame? lastGame, CricketGame? nextGame)> GetLastAndNextGamesForAustralia()
            => await _cricketRepository.GetLastAndNextGamesForTeam(await GetSeasonId(), AustralianTeamId);
    }
}
