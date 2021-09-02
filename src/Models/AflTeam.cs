using System.Collections.Generic;
using Newtonsoft.Json;

namespace afl_dakboard.Models
{
    public class AflTeam
    {
        [JsonConstructor]
        public AflTeam(
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

    public class AflTeamsRoot
    {
        [JsonConstructor]
        public AflTeamsRoot(List<AflTeam> teams)
        {
            Teams = teams;
        }

        [JsonProperty("teams")]
        public List<AflTeam> Teams { get; }
    }
}
