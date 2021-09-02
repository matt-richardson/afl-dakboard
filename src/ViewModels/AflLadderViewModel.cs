using System.Collections.Generic;
using afl_dakboard.Models;

namespace afl_dakboard.ViewModels
{
    public class AflLadderViewModel
    {
        public List<AflTeam> Teams { get; }
        public List<AflStanding> Standings { get; }

        public AflLadderViewModel(List<AflTeam> teams, List<AflStanding> standings)
        {
            Teams = teams;
            Standings = standings;
        }
    }
}
