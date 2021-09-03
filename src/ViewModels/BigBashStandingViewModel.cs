using afl_dakboard.Models;

namespace afl_dakboard.ViewModels
{
    public class BigBashStandingViewModel
    {
        public BigBashStandingViewModel(int order, BigBashTeam team, BigBashStanding standing)
        {
            Order = order;
            Team = team;
            Standing = standing;
        }

        public int Order { get; }
        public BigBashStanding Standing { get; }
        public BigBashTeam Team { get; }
    }
}
