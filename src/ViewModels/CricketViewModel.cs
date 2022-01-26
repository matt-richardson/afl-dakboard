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
        public int? OurTeamWickets { get; }
        public double? OurTeamOvers { get; }
        public int? OurTeamScore { get; }
        public int? OppositionWickets { get; }
        public double? OppositionOvers { get; }
        public int? OppositionScore { get; }
        public string? Opposition { get; }
        //usually our team, but if we're not in the finals, it changes
        public string? OurTeam { get; }
        public string? LastGameDate { get; }
        public string? LastGameRound { get; }
        public string Note { get; }
        public bool IsInProgress { get; }
        public string? NextGameDate { get; }
        public string? NextGameVenue { get; }
        public string? NextGameRound { get; }
        public string? NextGameTeam { get; }

        public CricketViewModel(CricketGame? lastGame, CricketGame? nextGame, ILogger logger, int ourTeamId, string ourTeamName)
        {
            var timezone = TZConvert.GetTimeZoneInfo("AUS Eastern Standard Time");
            var timeInMelbourne = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, timezone);
            if (lastGame != null)
            {
                var ourTeamRuns = lastGame.Runs.FirstOrDefault(x => x.TeamId == ourTeamId);
                (OurTeamScore, OurTeamWickets, OurTeamOvers) = ourTeamRuns != null
                    ? (ourTeamRuns.Score, ourTeamRuns.Wickets, ourTeamRuns.Overs)
                    : (0, 0, 0);

                var oppositionRuns = lastGame.Runs.FirstOrDefault(x => x.TeamId != ourTeamId);
                (OppositionScore, OppositionWickets, OppositionOvers) = oppositionRuns != null
                    ? (oppositionRuns.Score, oppositionRuns.Wickets, oppositionRuns.Overs)
                    : (0, 0, 0);

                Opposition = lastGame.LocalTeamId == ourTeamId
                    ? lastGame.VisitorTeam.Name
                    : lastGame.LocalTeam.Name;
                
                //this is ick. if we have been knocked out of the competition, then we may be showing other teams playing
                if (lastGame.IsTeam(ourTeamId))
                {
                    OurTeam = ourTeamName;
                }
                else
                {
                    OurTeam = lastGame.LocalTeamId == ourTeamId
                        ? lastGame.LocalTeam.Name
                        : lastGame.VisitorTeam.Name;
                }

                LastGameDate = TimeZoneInfo.ConvertTime(lastGame.StartingAt, timezone).Humanize(dateToCompareAgainst: timeInMelbourne.DateTime);
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
                var dateTime = TimeZoneInfo.ConvertTime(nextGame.StartingAt, timezone);
                NextGameDate = $"{dateTime:ddd MMM dd} at {dateTime:h:mm tt} ({dateTime.Humanize(dateToCompareAgainst: timeInMelbourne.DateTime)})";
                NextGameVenue = nextGame.Venue.Name;
                NextGameRound = nextGame.Round.ToLower();
                NextGameTeam = nextGame.LocalTeamId == ourTeamId ? nextGame.VisitorTeam.Name : nextGame.LocalTeam.Name;
            }

            logger.LogInformation("Rendering {Name} with {Json}", nameof(CricketViewModel), JsonConvert.SerializeObject(this));
        }
    }
}
