using afl_dakboard.Models;

namespace afl_dakboard.ViewModels
{
    public class BigBashStandingViewModel
    {
        public BigBashStandingViewModel(int order, CricketTeam team, CricketStanding standing)
        {
            Order = order;
            Team = team;
            Standing = standing;
        }

        public int Order { get; }
        public CricketStanding Standing { get; }
        public CricketTeam Team { get; }
    }
}
