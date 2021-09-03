using System.Collections.Generic;
using afl_dakboard.Models;

namespace afl_dakboard.ViewModels
{
    public class CricketLadderViewModel
    {
        public IReadOnlyList<CricketTeam> Teams { get; }
        public IReadOnlyList<CricketStanding> Standings { get; }

        public CricketLadderViewModel(IReadOnlyList<CricketTeam> teams, IReadOnlyList<CricketStanding> standings)
        {
            Teams = teams;
            Standings = standings;
        }
    }
}
