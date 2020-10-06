using System.Collections.Generic;

namespace afl_dakboard.Models
{
    public class Team    {
        public int id { get; set; }
        public string abbrev { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
    }

    public class TeamsRoot    {
        public List<Team> teams { get; set; }
    }
}
