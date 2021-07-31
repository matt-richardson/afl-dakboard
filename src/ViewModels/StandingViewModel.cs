using afl_dakboard.Models;

namespace afl_dakboard.ViewModels
{
    public class StandingViewModel
    {
        public StandingViewModel(int order, Team team, Standing standing)
        {
            Order = order;
            Team = team;
            Standing = standing;
        }

        public int Order { get; }
        public Standing Standing { get; }
        public Team Team { get; }
    }
}
