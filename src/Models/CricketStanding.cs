using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace afl_dakboard.Models
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Legend
    {
        [JsonConstructor]
        public Legend(
            [JsonProperty("resource")] string resource,
            [JsonProperty("id")] int id,
            [JsonProperty("stage_id")] int stageId,
            [JsonProperty("season_id")] int seasonId,
            [JsonProperty("league_id")] int leagueId,
            [JsonProperty("position")] int position,
            [JsonProperty("description")] string description,
            [JsonProperty("updated_at")] DateTime updatedAt
        )
        {
            Resource = resource;
            Id = id;
            StageId = stageId;
            SeasonId = seasonId;
            LeagueId = leagueId;
            Position = position;
            Description = description;
            UpdatedAt = updatedAt;
        }

        [JsonProperty("resource")]
        public string Resource { get; }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("stage_id")]
        public int StageId { get; }

        [JsonProperty("season_id")]
        public int SeasonId { get; }

        [JsonProperty("league_id")]
        public int LeagueId { get; }

        [JsonProperty("position")]
        public int Position { get; }

        [JsonProperty("description")]
        public string Description { get; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; }
    }

    public class CricketStanding
    {
        [JsonConstructor]
        public CricketStanding(
            [JsonProperty("resource")] string resource,
            [JsonProperty("legend_id")] int? legendId,
            [JsonProperty("team_id")] int teamId,
            [JsonProperty("stage_id")] int stageId,
            [JsonProperty("season_id")] int seasonId,
            [JsonProperty("league_id")] int leagueId,
            [JsonProperty("position")] int position,
            [JsonProperty("points")] int points,
            [JsonProperty("played")] int played,
            [JsonProperty("won")] int won,
            [JsonProperty("lost")] int lost,
            [JsonProperty("draw")] int draw,
            [JsonProperty("noresult")] int noResult,
            [JsonProperty("runs_for")] int runsFor,
            [JsonProperty("overs_for")] double oversFor,
            [JsonProperty("runs_against")] int runsAgainst,
            [JsonProperty("overs_against")] double oversAgainst,
            [JsonProperty("netto_run_rate")] double netToRunRate,
            [JsonProperty("recent_form")] List<object> recentForm,
            [JsonProperty("updated_at")] DateTime updatedAt,
            [JsonProperty("legend")] Legend legend
        )
        {
            Resource = resource;
            LegendId = legendId;
            TeamId = teamId;
            StageId = stageId;
            SeasonId = seasonId;
            LeagueId = leagueId;
            Position = position;
            Points = points;
            Played = played;
            Won = won;
            Lost = lost;
            Draw = draw;
            NoResult = noResult;
            RunsFor = runsFor;
            OversFor = oversFor;
            RunsAgainst = runsAgainst;
            OversAgainst = oversAgainst;
            NettoRunRate = netToRunRate;
            RecentForm = recentForm;
            UpdatedAt = updatedAt;
            Legend = legend;
        }

        [JsonProperty("resource")]
        public string Resource { get; }

        [JsonProperty("legend_id")]
        public int? LegendId { get; }

        [JsonProperty("team_id")]
        public int TeamId { get; }

        [JsonProperty("stage_id")]
        public int StageId { get; }

        [JsonProperty("season_id")]
        public int SeasonId { get; }

        [JsonProperty("league_id")]
        public int LeagueId { get; }

        [JsonProperty("position")]
        public int Position { get; }

        [JsonProperty("points")]
        public int Points { get; }

        [JsonProperty("played")]
        public int Played { get; }

        [JsonProperty("won")]
        public int Won { get; }

        [JsonProperty("lost")]
        public int Lost { get; }

        [JsonProperty("draw")]
        public int Draw { get; }

        [JsonProperty("noresult")]
        public int NoResult { get; }

        [JsonProperty("runs_for")]
        public int RunsFor { get; }

        [JsonProperty("overs_for")]
        public double OversFor { get; }

        [JsonProperty("runs_against")]
        public int RunsAgainst { get; }

        [JsonProperty("overs_against")]
        public double OversAgainst { get; }

        [JsonProperty("netto_run_rate")]
        public double NettoRunRate { get; }

        [JsonProperty("recent_form")]
        public IReadOnlyList<object> RecentForm { get; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; }

        [JsonProperty("legend")]
        public Legend Legend { get; }
    }

    public class CricketStandingsRoot
    {
        [JsonConstructor]
        public CricketStandingsRoot(
            [JsonProperty("data")] List<CricketStanding> data
        )
        {
            Standings = data;
        }

        [JsonProperty("data")]
        public IReadOnlyList<CricketStanding> Standings { get; }
    }
}
