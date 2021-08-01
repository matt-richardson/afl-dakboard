using System.Collections.Generic;
using Newtonsoft.Json;

namespace afl_dakboard.Models
{
    public class Team
    {
        [JsonConstructor]
        public Team(
            int id,
            string abbreviation,
            string name,
            string logo)
        {
            Id = id;
            Abbreviation = abbreviation;
            Name = name;
            Logo = logo;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("abbrev")]
        public string Abbreviation { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("logo")]
        public string Logo { get; }
    }

    public class TeamsRoot
    {
        [JsonConstructor]
        public TeamsRoot(List<Team> teams)
        {
            Teams = teams;
        }

        [JsonProperty("teams")]
        public List<Team> Teams { get; }
    }
}
