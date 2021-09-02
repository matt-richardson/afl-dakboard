using System.Collections.Generic;
using Newtonsoft.Json;

namespace afl_dakboard.Models
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AflStanding
    {
        [JsonConstructor]
        public AflStanding(
            int goalsFor,
            int wins,
            int @for,
            string name,
            int played,
            int pts,
            int rank,
            double percentage,
            int goalsAgainst,
            int id,
            int behindsFor,
            int against,
            int losses,
            int behindsAgainst,
            int draws)
        {
            GoalsFor = goalsFor;
            Wins = wins;
            For = @for;
            Name = name;
            Played = played;
            Pts = pts;
            Rank = rank;
            Percentage = percentage;
            GoalsAgainst = goalsAgainst;
            Id = id;
            BehindsFor = behindsFor;
            Against = against;
            Losses = losses;
            BehindsAgainst = behindsAgainst;
            Draws = draws;
        }

        [JsonProperty("goals_for")]
        public int GoalsFor { get; }

        [JsonProperty("wins")]
        public int Wins { get; }

        [JsonProperty("for")]
        public int For { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("played")]
        public int Played { get; }

        [JsonProperty("pts")]
        public int Pts { get; }

        [JsonProperty("rank")]
        public int Rank { get; }

        [JsonProperty("percentage")]
        public double Percentage { get; }

        [JsonProperty("goals_against")]
        public int GoalsAgainst { get; }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("behinds_for")]
        public int BehindsFor { get; }

        [JsonProperty("against")]
        public int Against { get; }

        [JsonProperty("losses")]
        public int Losses { get; }

        [JsonProperty("behinds_against")]
        public int BehindsAgainst { get; }

        [JsonProperty("draws")]
        public int Draws { get; }
    }

    public class AflStandingsRoot
    {
        [JsonConstructor]
        public AflStandingsRoot(List<AflStanding> standings)
        {
            Standings = standings;
        }

        [JsonProperty("standings")]
        public List<AflStanding> Standings { get; }
    }
}
