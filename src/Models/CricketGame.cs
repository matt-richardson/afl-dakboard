using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace afl_dakboard.Models
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Team
    {
        [JsonConstructor]
        public Team(
            [JsonProperty("resource")] string resource,
            [JsonProperty("id")] int id,
            [JsonProperty("name")] string name,
            [JsonProperty("code")] string code,
            [JsonProperty("image_path")] string imagePath,
            [JsonProperty("country_id")] int countryId,
            [JsonProperty("national_team")] bool nationalTeam,
            [JsonProperty("updated_at")] DateTime updatedAt
        )
        {
            Resource = resource;
            Id = id;
            Name = name;
            Code = code;
            ImagePath = imagePath;
            CountryId = countryId;
            NationalTeam = nationalTeam;
            UpdatedAt = updatedAt;
        }

        [JsonProperty("resource")]
        public string Resource { get; }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("code")]
        public string Code { get; }

        [JsonProperty("image_path")]
        public string ImagePath { get; }

        [JsonProperty("country_id")]
        public int CountryId { get; }

        [JsonProperty("national_team")]
        public bool NationalTeam { get; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; }
    }

    public class Run
    {
        [JsonConstructor]
        public Run(
            [JsonProperty("resource")] string resource,
            [JsonProperty("id")] int id,
            [JsonProperty("fixture_id")] int fixtureId,
            [JsonProperty("team_id")] int teamId,
            [JsonProperty("inning")] int inning,
            [JsonProperty("score")] int score,
            [JsonProperty("wickets")] int wickets,
            [JsonProperty("overs")] double overs,
            [JsonProperty("pp1")] string pp1,
            [JsonProperty("pp2")] object pp2,
            [JsonProperty("pp3")] object pp3,
            [JsonProperty("updated_at")] DateTime updatedAt
        )
        {
            Resource = resource;
            Id = id;
            FixtureId = fixtureId;
            TeamId = teamId;
            Inning = inning;
            Score = score;
            Wickets = wickets;
            Overs = overs;
            Pp1 = pp1;
            Pp2 = pp2;
            Pp3 = pp3;
            UpdatedAt = updatedAt;
        }

        [JsonProperty("resource")]
        public string Resource { get; }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("fixture_id")]
        public int FixtureId { get; }

        [JsonProperty("team_id")]
        public int TeamId { get; }

        [JsonProperty("inning")]
        public int Inning { get; }

        [JsonProperty("score")]
        public int Score { get; }

        [JsonProperty("wickets")]
        public int Wickets { get; }

        [JsonProperty("overs")]
        public double Overs { get; }

        [JsonProperty("pp1")]
        public string Pp1 { get; }

        [JsonProperty("pp2")]
        public object Pp2 { get; }

        [JsonProperty("pp3")]
        public object Pp3 { get; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; }
    }

    public class Venue
    {
        [JsonConstructor]
        public Venue(
            [JsonProperty("resource")] string resource,
            [JsonProperty("id")] int id,
            [JsonProperty("country_id")] int countryId,
            [JsonProperty("name")] string name,
            [JsonProperty("city")] string city,
            [JsonProperty("image_path")] string imagePath,
            [JsonProperty("capacity")] int? capacity,
            [JsonProperty("floodlight")] bool floodlight,
            [JsonProperty("updated_at")] DateTime updatedAt
        )
        {
            Resource = resource;
            Id = id;
            CountryId = countryId;
            Name = name;
            City = city;
            ImagePath = imagePath;
            Capacity = capacity;
            Floodlight = floodlight;
            UpdatedAt = updatedAt;
        }

        [JsonProperty("resource")]
        public string Resource { get; }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("country_id")]
        public int CountryId { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("city")]
        public string City { get; }

        [JsonProperty("image_path")]
        public string ImagePath { get; }

        [JsonProperty("capacity")]
        public int? Capacity { get; }

        [JsonProperty("floodlight")]
        public bool Floodlight { get; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; }
    }

    public class CricketGame
    {
        [JsonConstructor]
        public CricketGame(
            [JsonProperty("resource")] string resource,
            [JsonProperty("id")] int id,
            [JsonProperty("league_id")] int leagueId,
            [JsonProperty("season_id")] int seasonId,
            [JsonProperty("stage_id")] int stageId,
            [JsonProperty("round")] string round,
            [JsonProperty("localteam_id")] int localTeamId,
            [JsonProperty("visitorteam_id")] int visitorTeamId,
            [JsonProperty("starting_at")] DateTime startingAt,
            [JsonProperty("type")] string type,
            [JsonProperty("live")] bool live,
            [JsonProperty("status")] string status,
            [JsonProperty("last_period")] object lastPeriod,
            [JsonProperty("note")] string note,
            [JsonProperty("venue_id")] int? venueId,
            [JsonProperty("toss_won_team_id")] int? tossWonTeamId,
            [JsonProperty("winner_team_id")] int? winnerTeamId,
            [JsonProperty("draw_noresult")] object drawNoResult,
            [JsonProperty("first_umpire_id")] object firstUmpireId,
            [JsonProperty("second_umpire_id")] int? secondUmpireId,
            [JsonProperty("tv_umpire_id")] object tvUmpireId,
            [JsonProperty("referee_id")] object refereeId,
            [JsonProperty("man_of_match_id")] object manOfMatchId,
            [JsonProperty("man_of_series_id")] object manOfSeriesId,
            [JsonProperty("total_overs_played")] int? totalOversPlayed,
            [JsonProperty("elected")] string elected,
            [JsonProperty("super_over")] bool superOver,
            [JsonProperty("follow_on")] bool followOn,
            [JsonProperty("rpc_overs")] object rpcOvers,
            [JsonProperty("rpc_target")] object rpcTarget,
            [JsonProperty("weather_report")] List<object> weatherReport,
            [JsonProperty("localteam")] Team localTeam,
            [JsonProperty("visitorteam")] Team visitorTeam,
            [JsonProperty("runs")] List<Run> runs,
            [JsonProperty("venue")] Venue venue
        )
        {
            Resource = resource;
            Id = id;
            LeagueId = leagueId;
            SeasonId = seasonId;
            StageId = stageId;
            Round = round;
            LocalTeamId = localTeamId;
            VisitorTeamId = visitorTeamId;
            StartingAt = startingAt;
            Type = type;
            Live = live;
            Status = status;
            LastPeriod = lastPeriod;
            Note = note;
            VenueId = venueId;
            TossWonTeamId = tossWonTeamId;
            WinnerTeamId = winnerTeamId;
            DrawNoResult = drawNoResult;
            FirstUmpireId = firstUmpireId;
            SecondUmpireId = secondUmpireId;
            TvUmpireId = tvUmpireId;
            RefereeId = refereeId;
            ManOfMatchId = manOfMatchId;
            ManOfSeriesId = manOfSeriesId;
            TotalOversPlayed = totalOversPlayed;
            Elected = elected;
            SuperOver = superOver;
            FollowOn = followOn;
            RpcOvers = rpcOvers;
            RpcTarget = rpcTarget;
            WeatherReport = weatherReport;
            Runs = runs;
            Venue = venue;
            LocalTeam = localTeam;
            VisitorTeam = visitorTeam;
        }

        [JsonProperty("resource")]
        public string Resource { get; }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("league_id")]
        public int LeagueId { get; }

        [JsonProperty("season_id")]
        public int SeasonId { get; }

        [JsonProperty("stage_id")]
        public int StageId { get; }

        [JsonProperty("round")]
        public string Round { get; }

        [JsonProperty("localteam_id")]
        public int LocalTeamId { get; }

        [JsonProperty("visitorteam_id")]
        public int VisitorTeamId { get; }

        [JsonProperty("starting_at")]
        public DateTime StartingAt { get; }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("live")]
        public bool Live { get; }

        [JsonProperty("status")]
        public string Status { get; }

        [JsonProperty("last_period")]
        public object LastPeriod { get; }

        [JsonProperty("note")]
        public string Note { get; }

        [JsonProperty("venue_id")]
        public int? VenueId { get; }

        [JsonProperty("toss_won_team_id")]
        public int? TossWonTeamId { get; }

        [JsonProperty("winner_team_id")]
        public int? WinnerTeamId { get; }

        [JsonProperty("draw_noresult")]
        public object DrawNoResult { get; }

        [JsonProperty("first_umpire_id")]
        public object FirstUmpireId { get; }

        [JsonProperty("second_umpire_id")]
        public int? SecondUmpireId { get; }

        [JsonProperty("tv_umpire_id")]
        public object TvUmpireId { get; }

        [JsonProperty("referee_id")]
        public object RefereeId { get; }

        [JsonProperty("man_of_match_id")]
        public object ManOfMatchId { get; }

        [JsonProperty("man_of_series_id")]
        public object ManOfSeriesId { get; }

        [JsonProperty("total_overs_played")]
        public int? TotalOversPlayed { get; }

        [JsonProperty("elected")]
        public string Elected { get; }

        [JsonProperty("super_over")]
        public bool SuperOver { get; }

        [JsonProperty("follow_on")]
        public bool FollowOn { get; }

        [JsonProperty("rpc_overs")]
        public object RpcOvers { get; }

        [JsonProperty("rpc_target")]
        public object RpcTarget { get; }

        [JsonProperty("weather_report")]
        public IReadOnlyList<object> WeatherReport { get; }

        [JsonProperty("localteam")]
        public Team LocalTeam { get; }

        [JsonProperty("visitorteam")]
        public Team VisitorTeam { get; }

        [JsonProperty("runs")]
        public IReadOnlyList<Run> Runs { get; }

        [JsonProperty("venue")]
        public Venue Venue { get; }
    }

    public static class CricketGameExtensions
    {
        public static bool IsComplete(this CricketGame? cricketGame)
        {
            if (cricketGame == null) return false;
            return cricketGame.Status switch
            {
                "Finished" => true,
                "Aban." => true,
                "Cancl." => true,
                "Postp." => true,
                "NS" => false,
                "1st Innings" => false,
                "2nd Innings" => false,
                "Innings Break" => false,
                _ => throw new ApplicationException($"Unknown game status: {cricketGame.Status}")
            };
        }        
        
        public static bool IsTeam(this CricketGame cricketGame, int teamId)
        {
            return cricketGame.LocalTeamId == teamId || cricketGame.VisitorTeamId == teamId;
        }
        
        public static bool IsInProgress(this CricketGame? cricketGame)
        {
            if (cricketGame == null) return false;
            return cricketGame.Status switch
            {
                "Finished" => false,
                "Aban." => false,
                "Cancl." => false,
                "Postp." => false,
                "NS" => false,
                "1st Innings" => true,
                "2nd Innings" => true,
                "Innings Break" => true,
                _ => throw new ApplicationException($"Unknown game status: {cricketGame.Status}")
            };
        }
    }
    
    public class Links
    {
        [JsonConstructor]
        public Links(
            [JsonProperty("first")] string first,
            [JsonProperty("last")] string last,
            [JsonProperty("prev")] string prev,
            [JsonProperty("next")] string next,
            [JsonProperty("url")] string url,
            [JsonProperty("label")] string label,
            [JsonProperty("active")] bool active
        )
        {
            First = first;
            Last = last;
            Prev = prev;
            Next = next;
            Url = url;
            Label = label;
            Active = active;
        }

        [JsonProperty("first")]
        public string First { get; }

        [JsonProperty("last")]
        public string Last { get; }

        [JsonProperty("prev")]
        public string Prev { get; }

        [JsonProperty("next")]
        public string Next { get; }

        [JsonProperty("url")]
        public string Url { get; }

        [JsonProperty("label")]
        public string Label { get; }

        [JsonProperty("active")]
        public bool Active { get; }
    }

    public class Link
    {
        [JsonConstructor]
        public Link(
            [JsonProperty("url")] string url,
            [JsonProperty("label")] string label,
            [JsonProperty("active")] bool active
        )
        {
            Url = url;
            Label = label;
            Active = active;
        }

        [JsonProperty("url")]
        public string Url { get; }

        [JsonProperty("label")]
        public string Label { get; }

        [JsonProperty("active")]
        public bool Active { get; }
    }

    public class Meta
    {
        [JsonConstructor]
        public Meta(
            [JsonProperty("current_page")] int currentPage,
            [JsonProperty("from")] int? from,
            [JsonProperty("last_page")] int lastPage,
            [JsonProperty("links")] List<Link> links,
            [JsonProperty("path")] string path,
            [JsonProperty("per_page")] int perPage,
            [JsonProperty("to")] int? to,
            [JsonProperty("total")] int total
        )
        {
            CurrentPage = currentPage;
            From = from;
            LastPage = lastPage;
            Links = links;
            Path = path;
            PerPage = perPage;
            To = to;
            Total = total;
        }

        [JsonProperty("current_page")]
        public int CurrentPage { get; }

        [JsonProperty("from")]
        public int? From { get; }

        [JsonProperty("last_page")]
        public int LastPage { get; }

        [JsonProperty("links")]
        public IReadOnlyList<Link> Links { get; }

        [JsonProperty("path")]
        public string Path { get; }

        [JsonProperty("per_page")]
        public int PerPage { get; }

        [JsonProperty("to")]
        public int? To { get; }

        [JsonProperty("total")]
        public int Total { get; }
    }

    public class CricketGamesRoot
    {
        [JsonConstructor]
        public CricketGamesRoot(
            [JsonProperty("data")] List<CricketGame> data,
            [JsonProperty("links")] Links links,
            [JsonProperty("meta")] Meta meta
        )
        {
            Games = data;
            Links = links;
            Meta = meta;
        }

        [JsonProperty("data")]
        public IReadOnlyList<CricketGame> Games { get; }

        [JsonProperty("links")]
        public Links Links { get; }

        [JsonProperty("meta")]
        public Meta Meta { get; }
    }
}
