using System.Collections.Generic;
using Newtonsoft.Json;

namespace afl_dakboard.Models
{
    public class Team    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("abbrev")]
        public string Abbreviation { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }
    }

    public class TeamsRoot    {
        [JsonProperty("teams")]
        public List<Team> Teams { get; set; }
    }
}
