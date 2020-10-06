using System.Collections.Generic;

namespace afl_dakboard.Models
{
    public class Game    {
        public int is_final { get; set; }
        public string roundname { get; set; }
        public int hteamid { get; set; }
        public int? winnerteamid { get; set; }
        public int is_grand_final { get; set; }
        public int year { get; set; }
        public string updated { get; set; }
        public int? hgoals { get; set; }
        public int round { get; set; }
        public string venue { get; set; }
        public int? ascore { get; set; }
        public int ateamid { get; set; }
        public int? hscore { get; set; }
        public string winner { get; set; }
        public string ateam { get; set; }
        public string hteam { get; set; }
        public int? abehinds { get; set; }
        public string tz { get; set; }
        public int id { get; set; }
        public int complete { get; set; }
        public int? agoals { get; set; }
        public int? hbehinds { get; set; }
        public string date { get; set; }
    }

    public class GamesRoot    {
        public List<Game> games { get; set; }
    }
}
