using System;
using System.Linq;
using afl_dakboard.Models;
using Humanizer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TimeZoneConverter;

namespace afl_dakboard.ViewModels
{
    public class MelbourneStarsViewModel
    {
        private const int BigBashMelbourneStarsTeamId = 51;

        public int? MelbourneStarsWickets { get; }
        public double MelbourneStarsOvers { get; }
        public int? MelbourneStarsScore { get; }
        public int? OppositionWickets { get; }
        public double OppositionOvers { get; }
        public int? OppositionScore { get; }
        public string Opposition { get; }
        public string LastGameDate { get; }
        public string Note { get; }
        public string? NextGameDate { get; }
        public string? NextGameVenue { get; }
        public string NextGameRound { get; }
        public string? NextGameTeam { get; }

        public MelbourneStarsViewModel(BigBashGame lastGame, BigBashGame? nextGame, ILogger logger)
        {
            var melbourneStarsRuns = lastGame.Runs.FirstOrDefault(x => x.TeamId == BigBashMelbourneStarsTeamId);
            (MelbourneStarsScore, MelbourneStarsWickets, MelbourneStarsOvers) = melbourneStarsRuns != null
                ? (melbourneStarsRuns.Score, melbourneStarsRuns.Wickets, melbourneStarsRuns.Overs)
                : (0, 0, 0);

            var oppositionRuns = lastGame.Runs.FirstOrDefault(x => x.TeamId != BigBashMelbourneStarsTeamId);
            (OppositionScore, OppositionWickets, OppositionOvers) = oppositionRuns != null
                ? (oppositionRuns.Score, oppositionRuns.Wickets, oppositionRuns.Overs)
                : (0, 0, 0);

            Opposition = lastGame.LocalTeamId == BigBashMelbourneStarsTeamId
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
                NextGameTeam = nextGame.LocalTeamId == BigBashMelbourneStarsTeamId ? nextGame.VisitorTeam.Name : nextGame.LocalTeam.Name;
            }

            logger.LogInformation("Rendering {Name} with {Json}", nameof(MelbourneStarsViewModel), JsonConvert.SerializeObject(this));
        }
    }
}
