using System.Collections.Generic;
using afl_dakboard.Models;

namespace afl_dakboard.ViewModels
{
    public class BigBashLadderViewModel
    {
        public IReadOnlyList<BigBashTeam> Teams { get; }
        public IReadOnlyList<BigBashStanding> Standings { get; }

        public BigBashLadderViewModel(IReadOnlyList<BigBashTeam> teams, IReadOnlyList<BigBashStanding> standings)
        {
            Teams = teams;
            Standings = standings;
        }
    }
}
