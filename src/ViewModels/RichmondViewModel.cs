using System;
using System.Linq;
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
        public string NextGameDate { get; }
        public string NextGameVenue { get; }

        public RichmondViewModel(Game lastGame, Game nextGame, ILogger logger)
        {
            if (lastGame.hteam == "Richmond")
            {
                (RichmondGoals, RichmondBehinds, RichmondScore) = (lastGame.hgoals, lastGame.hbehinds, lastGame.hscore);
                (OppositionGoals, OppositionBehinds, OppositionScore, Opposition) = (lastGame.agoals, lastGame.abehinds, lastGame.ascore, lastGame.ateam);
            }
            else
            {
                (RichmondGoals, RichmondBehinds, RichmondScore) = (lastGame.agoals, lastGame.abehinds, lastGame.ascore);
                (OppositionGoals, OppositionBehinds, OppositionScore, Opposition) = (lastGame.hgoals, lastGame.hbehinds, lastGame.hscore, lastGame.hteam);
            }

            var timezone = TZConvert.GetTimeZoneInfo("AUS Eastern Standard Time");
            var timeInMelbourne = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, timezone);
            LastGameDate = DateTime.Parse(lastGame.date).Humanize(dateToCompareAgainst: timeInMelbourne.DateTime);

            var dateTime = DateTime.Parse(nextGame.date);
            NextGameDate = $"{dateTime:ddd MMM dd} at {dateTime:h:mm tt} ({dateTime.Humanize(dateToCompareAgainst: timeInMelbourne.DateTime)})";
            NextGameVenue = nextGame.venue;
            logger.LogInformation("Rendering {name} with {json}", nameof(RichmondViewModel), JsonConvert.SerializeObject(this));
        }
    }
}
