using afl_dakboard.Models;

namespace afl_dakboard.ViewModels
{
    public class AflStandingViewModel
    {
        public AflStandingViewModel(int order, AflTeam team, AflStanding standing)
        {
            Order = order;
            Team = team;
            Standing = standing;
        }

        public int Order { get; }
        public AflStanding Standing { get; }
        public AflTeam Team { get; }
    }
}
