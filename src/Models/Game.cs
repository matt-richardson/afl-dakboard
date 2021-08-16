using System.Collections.Generic;
using Newtonsoft.Json;

namespace afl_dakboard.Models
{
    public class Game
    {
        [JsonConstructor]
        public Game(
            int isFinal,
            string roundName,
            int? homeTeamId,
            int? winnerTeamId,
            int isGrandFinal,
            int year,
            string updated,
            int? homeGoals,
            int round,
            string venue,
            int? awayScore,
            int? awayTeamId,
            int? homeScore,
            string winner,
            string awayTeam,
            string homeTeam,
            int? awayBehinds,
            string tz,
            int id,
            int complete,
            int? awayGoals,
            int? homeBehinds,
            string date)
        {
            IsFinal = isFinal;
            RoundName = roundName;
            HomeTeamId = homeTeamId;
            WinnerTeamId = winnerTeamId;
            IsGrandFinal = isGrandFinal;
            Year = year;
            Updated = updated;
            HomeGoals = homeGoals;
            Round = round;
            Venue = venue;
            AwayScore = awayScore;
            AwayTeamId = awayTeamId;
            HomeScore = homeScore;
            Winner = winner;
            AwayTeam = awayTeam;
            HomeTeam = homeTeam;
            AwayBehinds = awayBehinds;
            Tz = tz;
            Id = id;
            Complete = complete;
            AwayGoals = awayGoals;
            HomeBehinds = homeBehinds;
            Date = date;
        }

        [JsonProperty("is_final")]
        public int IsFinal { get; }

        [JsonProperty("roundname")]
        public string RoundName { get; }

        [JsonProperty("hteamid")]
        public int? HomeTeamId { get; }

        [JsonProperty("winnerteamid")]
        public int? WinnerTeamId { get; }

        [JsonProperty("is_grand_final")]
        public int IsGrandFinal { get; }

        [JsonProperty("year")]
        public int Year { get; }

        [JsonProperty("updated")]
        public string Updated { get; }

        [JsonProperty("hgoals")]
        public int? HomeGoals { get; }

        [JsonProperty("round")]
        public int Round { get; }

        [JsonProperty("venue")]
        public string Venue { get; }

        [JsonProperty("ascore")]
        public int? AwayScore { get; }

        [JsonProperty("ateamid")]
        public int? AwayTeamId { get; }

        [JsonProperty("hscore")]
        public int? HomeScore { get; }

        [JsonProperty("winner")]
        public string Winner { get; }

        [JsonProperty("ateam")]
        public string AwayTeam { get; }

        [JsonProperty("hteam")]
        public string HomeTeam { get; }

        [JsonProperty("abehinds")]
        public int? AwayBehinds { get; }

        [JsonProperty("tz")]
        public string Tz { get; }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("complete")]
        public int Complete { get; }

        [JsonProperty("agoals")]
        public int? AwayGoals { get; }

        [JsonProperty("hbehinds")]
        public int? HomeBehinds { get; }

        [JsonProperty("date")]
        public string Date { get; }
    }

    public class GamesRoot
    {
        [JsonConstructor]
        public GamesRoot(List<Game> games)
        {
            Games = games;
        }

        public List<Game> Games { get; }
    }
}
