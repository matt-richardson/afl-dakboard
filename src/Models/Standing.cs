using System.Collections.Generic;
using Newtonsoft.Json;

namespace afl_dakboard.Models
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Standing    {
        [JsonProperty("goals_for")]
        public int GoalsFor { get; set; }

        [JsonProperty("wins")]
        public int Wins { get; set; }

        [JsonProperty("for")]
        public int For { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("played")]
        public int Played { get; set; }

        [JsonProperty("pts")]
        public int Pts { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("percentage")]
        public double Percentage { get; set; }

        [JsonProperty("goals_against")]
        public int GoalsAgainst { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("behinds_for")]
        public int BehindsFor { get; set; }

        [JsonProperty("against")]
        public int Against { get; set; }

        [JsonProperty("losses")]
        public int Losses { get; set; }

        [JsonProperty("behinds_against")]
        public int BehindsAgainst { get; set; }

        [JsonProperty("draws")]
        public int Draws { get; set; }
    }

    public class StandingsRoot    {
        [JsonProperty("standings")]
        public List<Standing> Standings { get; set; }
    }
}
