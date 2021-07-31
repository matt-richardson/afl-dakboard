using System.Collections.Generic;
using afl_dakboard.Models;

namespace afl_dakboard.ViewModels
{
    public class IndexViewModel
    {
        public List<Team> Teams { get; }
        public List<Standing> Standings { get; }

        public IndexViewModel(List<Team> teams, List<Standing> standings)
        {
            Teams = teams;
            Standings = standings;
        }
    }
}
