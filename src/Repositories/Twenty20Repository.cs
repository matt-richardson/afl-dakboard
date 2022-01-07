using System.Collections.Generic;
using System.Threading.Tasks;
using afl_dakboard.Models;

namespace afl_dakboard.Repositories
{
    public class Twenty20Repository
    {
        //Twenty20 International == league_id 3
        //  2021 = season_id 507
        //  2022 = season_id 782

        private const int Season2021Id = 507;
        private const int AustralianTeamId = 36;

        private readonly CricketRepository _cricketRepository;

        public Twenty20Repository(CricketRepository cricketRepository)
        {
            _cricketRepository = cricketRepository;
        }

        public int TeamId => AustralianTeamId;

        public async Task<IReadOnlyList<CricketTeam>> GetTeams()
            => await _cricketRepository.GetTeams();

        public async Task<IReadOnlyList<CricketStanding>> GetStandings()
            => await _cricketRepository.GetStandings(Season2021Id);

        public async Task<(CricketGame? lastGame, CricketGame? nextGame)> GetLastAndNextGamesForAustralia()
            => await _cricketRepository.GetLastAndNextGamesForTeam(Season2021Id, AustralianTeamId);
    }
}
