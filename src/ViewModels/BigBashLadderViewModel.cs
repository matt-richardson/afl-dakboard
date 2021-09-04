using System.Collections.Generic;
using afl_dakboard.Models;

namespace afl_dakboard.ViewModels
{
    public class BigBashLadderViewModel
    {
        public IReadOnlyList<CricketTeam> Teams { get; }
        public IReadOnlyList<CricketStanding> Standings { get; }

        public BigBashLadderViewModel(IReadOnlyList<CricketTeam> teams, IReadOnlyList<CricketStanding> standings)
        {
            Teams = teams;
            Standings = standings;
        }
    }
}
