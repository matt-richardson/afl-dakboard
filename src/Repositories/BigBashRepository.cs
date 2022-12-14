using System.Collections.Generic;
using System.Threading.Tasks;
using afl_dakboard.Models;

namespace afl_dakboard.Repositories
{
    public class BigBashRepository
    {
        //Big Bash League == league_id = 5
        //  2020/2021 = season_id = 525
        //  2021/2022 = season_id = 830
        //  2022/2023 = season_id = 1079        //todo: look this up automatically
        //  Melbourne Stars = team_id = 51

        private const int BigBash20212022SeasonId = 1079;
        private const int BigBashMelbourneStarsTeamId = 51;

        private readonly CricketRepository _cricketRepository;

        public BigBashRepository(CricketRepository cricketRepository)
        {
            _cricketRepository = cricketRepository;
        }

        public int TeamId => BigBashMelbourneStarsTeamId;

        public async Task<IReadOnlyList<CricketTeam>> GetTeams()
            => await _cricketRepository.GetTeams();

        public async Task<IReadOnlyList<CricketStanding>> GetStandings()
            => await _cricketRepository.GetStandings(BigBash20212022SeasonId);

        public async Task<(CricketGame? lastGame, CricketGame? nextGame)> GetLastAndNextGamesForMelbourneStars()
            => await _cricketRepository.GetLastAndNextGamesForTeam(BigBash20212022SeasonId, BigBashMelbourneStarsTeamId);
    }
}
