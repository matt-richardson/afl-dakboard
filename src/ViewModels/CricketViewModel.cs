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
        public double OurTeamOvers { get; }
        public int? OurTeamScore { get; }
        public int? OppositionWickets { get; }
        public double OppositionOvers { get; }
        public int? OppositionScore { get; }
        public string Opposition { get; }
        public string LastGameDate { get; }
        public string Note { get; }
        public string? NextGameDate { get; }
        public string? NextGameVenue { get; }
        public string? NextGameRound { get; }
        public string? NextGameTeam { get; }

        public CricketViewModel(CricketGame lastGame, CricketGame? nextGame, ILogger logger, int ourTeamId)
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

            var timezone = TZConvert.GetTimeZoneInfo("AUS Eastern Standard Time");
            var timeInMelbourne = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, timezone);
            LastGameDate = lastGame.StartingAt.Humanize(dateToCompareAgainst: timeInMelbourne.DateTime);
            Note = lastGame.Note;

            if (nextGame != null)
            {
                var dateTime = nextGame.StartingAt;
                NextGameDate = $"{dateTime:ddd MMM dd} at {dateTime:h:mm tt} ({dateTime.Humanize(dateToCompareAgainst: timeInMelbourne.DateTime)})";
                NextGameVenue = nextGame.Venue.Name;
                NextGameRound = nextGame.Round;
                NextGameTeam = nextGame.LocalTeamId == ourTeamId ? nextGame.VisitorTeam.Name : nextGame.LocalTeam.Name;
            }

            logger.LogInformation("Rendering {Name} with {Json}", nameof(CricketViewModel), JsonConvert.SerializeObject(this));
        }
    }
}
