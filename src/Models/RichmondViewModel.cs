using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using afl_dakboard.Models;
using Humanizer;

namespace afl_dakboard.Controllers
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

        public RichmondViewModel(Game lastGame, Game nextGame)
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

            LastGameDate = DateTime.Parse(lastGame.date).Humanize(utcDate: false);

            var dateTime = DateTime.Parse(nextGame.date);
            NextGameDate = $"{dateTime:ddd MMM dd} at {dateTime:h:mm tt} ({dateTime.Humanize(utcDate: false)})";
            NextGameVenue = nextGame.venue;
        }
    }
}
