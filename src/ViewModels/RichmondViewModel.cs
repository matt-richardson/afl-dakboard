using System;
using afl_dakboard.Models;
using Humanizer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TimeZoneConverter;

namespace afl_dakboard.ViewModels
{
    public class RichmondViewModel
    {
        public int? RichmondGoals { get; }
        public int? RichmondBehinds { get; }
        public int? RichmondScore { get; }
        public int? OppositionGoals { get; }
        public int? OppositionBehinds { get; }
        public int? OppositionScore { get; }
        public string Opposition { get; }
        public string LastGameDate { get; }
        public string? NextGameDate { get; }
        public string? NextGameVenue { get; }
        public int NextGameRound { get; }
        public string? NextGameTeam { get; }

        public RichmondViewModel(Game lastGame, Game? nextGame, ILogger logger)
        {
            if (lastGame.HomeTeam == "Richmond")
            {
                (RichmondGoals, RichmondBehinds, RichmondScore) = (lastGame.HomeGoals, lastGame.HomeBehinds, lastGame.HomeScore);
                (OppositionGoals, OppositionBehinds, OppositionScore, Opposition) = (lastGame.AwayGoals, lastGame.AwayBehinds, lastGame.AwayScore, lastGame.AwayTeam);
            }
            else
            {
                (RichmondGoals, RichmondBehinds, RichmondScore) = (lastGame.AwayGoals, lastGame.AwayBehinds, lastGame.AwayScore);
                (OppositionGoals, OppositionBehinds, OppositionScore, Opposition) = (lastGame.HomeGoals, lastGame.HomeBehinds, lastGame.HomeScore, lastGame.HomeTeam);
            }

            var timezone = TZConvert.GetTimeZoneInfo("AUS Eastern Standard Time");
            var timeInMelbourne = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, timezone);
            LastGameDate = DateTime.Parse(lastGame.Date).Humanize(dateToCompareAgainst: timeInMelbourne.DateTime);

            if (nextGame != null)
            {
                var dateTime = DateTime.Parse(nextGame.Date);
                NextGameDate = $"{dateTime:ddd MMM dd} at {dateTime:h:mm tt} ({dateTime.Humanize(dateToCompareAgainst: timeInMelbourne.DateTime)})";
                NextGameVenue = nextGame.Venue;
                NextGameRound = nextGame.Round;
                NextGameTeam = nextGame.HomeTeam == "Richmond" ? nextGame.AwayTeam : nextGame.HomeTeam;
            }

            logger.LogInformation("Rendering {Name} with {Json}", nameof(RichmondViewModel), JsonConvert.SerializeObject(this));
        }
    }
}
