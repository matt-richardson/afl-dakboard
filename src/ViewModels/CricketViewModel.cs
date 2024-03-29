using System;
using System.Linq;
using afl_dakboard.Models;
using Humanizer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TimeZoneConverter;

namespace afl_dakboard.ViewModels
{
    public class CricketViewModel
    {
        //usually our team, but if we're not in the finals, it changes
        public string? HomeTeam { get; }
        public int? HomeTeamWickets { get; }
        public double? HomeTeamOvers { get; }
        public int? HomeTeamScore { get; }
        public int? OppositionWickets { get; }
        public double? OppositionOvers { get; }
        public int? OppositionScore { get; }
        public string? Opposition { get; }
        public string? LastGameDate { get; }
        public string? LastGameRound { get; }
        public string Note { get; }
        public bool IsInProgress { get; }
        public string? NextGameDate { get; }
        public string? NextGameVenue { get; }
        public string? NextGameRound { get; }
        public string? NextGameTeam { get; }

        public CricketViewModel(CricketGame? lastGame, CricketGame? nextGame, ILogger logger, int ourTeamId)
        {
            var timezone = TZConvert.GetTimeZoneInfo("AUS Eastern Standard Time");
            var convertedTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, timezone);
            var timeInMelbourne = DateTime.SpecifyKind(convertedTime.DateTime, DateTimeKind.Local);
            
            if (lastGame != null)
            {
                var (homeTeam, oppositionTeam) = GetTeams(lastGame, ourTeamId);
                HomeTeam = homeTeam.Name;
                Opposition = oppositionTeam.Name;
                
                var homeTeamRuns = lastGame.Runs.FirstOrDefault(x => x.TeamId == homeTeam.Id);
                (HomeTeamScore, HomeTeamWickets, HomeTeamOvers) = homeTeamRuns != null
                    ? (homeTeamRuns.Score, homeTeamRuns.Wickets, homeTeamRuns.Overs)
                    : (0, 0, 0);

                var oppositionRuns = lastGame.Runs.FirstOrDefault(x => x.TeamId == oppositionTeam.Id);
                (OppositionScore, OppositionWickets, OppositionOvers) = oppositionRuns != null
                    ? (oppositionRuns.Score, oppositionRuns.Wickets, oppositionRuns.Overs)
                    : (0, 0, 0);

                var dateTime = TimeZoneInfo.ConvertTime(lastGame.StartingAt, timezone);
                LastGameDate = dateTime.Humanize(dateToCompareAgainst: timeInMelbourne);
                Note = lastGame.Note;
                IsInProgress = lastGame.IsInProgress();
                LastGameRound = lastGame.Round;
            }
            else
            {
                Note = "Season hasn't started yet";
            }

            if (nextGame != null)
            {
                var dateTime = DateTime.SpecifyKind(TimeZoneInfo.ConvertTime(nextGame.StartingAt, timezone), DateTimeKind.Local);
                var when = dateTime.Humanize(dateToCompareAgainst: timeInMelbourne);
                NextGameDate = $"{dateTime:ddd MMM dd} at {dateTime:h:mm tt} ({when})";
                NextGameVenue = nextGame.Venue?.Name;
                NextGameRound = nextGame.Round.ToLower();
                NextGameTeam = nextGame.LocalTeamId == ourTeamId ? nextGame.VisitorTeam.Name : nextGame.LocalTeam.Name;
            }

            logger.LogInformation("Rendering {Name} with {@Model}", nameof(CricketViewModel), this);
        }

        private static (Team LocalTeam, Team VisitorTeam) GetTeams(CricketGame lastGame, int ourTeamId)
        {
            //if we have been knocked out of the competition, then we show other teams playing
            if (lastGame.IsTeam(ourTeamId))
            {
                return lastGame.LocalTeamId == ourTeamId
                    ? (lastGame.LocalTeam, lastGame.VisitorTeam)
                    : (lastGame.VisitorTeam, lastGame.LocalTeam);
            }

            return (lastGame.LocalTeam, lastGame.VisitorTeam);
        }
    }
}
