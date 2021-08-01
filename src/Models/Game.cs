using System.Collections.Generic;
using Newtonsoft.Json;

namespace afl_dakboard.Models
{
    public class Game    {
        [JsonProperty("is_final")]
        public int IsFinal { get; set; }

        [JsonProperty("roundname")]
        public string RoundName { get; set; }

        [JsonProperty("hteamid")]
        public int HomeTeamId { get; set; }

        [JsonProperty("winnerteamid")]
        public int? WinnerTeamId { get; set; }

        [JsonProperty("is_grand_final")]
        public int IsGrandFinal { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("hgoals")]
        public int? HomeGoals { get; set; }

        [JsonProperty("round")]
        public int Round { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("ascore")]
        public int? AwayScore { get; set; }

        [JsonProperty("ateamid")]
        public int AwayTeamId { get; set; }

        [JsonProperty("hscore")]
        public int? HomeScore { get; set; }

        [JsonProperty("winner")]
        public string Winner { get; set; }

        [JsonProperty("ateam")]
        public string AwayTeam { get; set; }

        [JsonProperty("hteam")]
        public string HomeTeam { get; set; }

        [JsonProperty("abehinds")]
        public int? AwayBehinds { get; set; }

        [JsonProperty("tz")]
        public string Tz { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("complete")]
        public int Complete { get; set; }

        [JsonProperty("agoals")]
        public int? AwayGoals { get; set; }

        [JsonProperty("hbehinds")]
        public int? HomeBehinds { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }

    public class GamesRoot    {
        public List<Game> Games { get; set; }
    }
}
