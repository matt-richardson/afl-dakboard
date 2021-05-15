using System;
using System.Collections.Generic;
using System.Linq;
using afl_dakboard.Models;
using Humanizer;

namespace afl_dakboard.Controllers
{
    public class RichmondViewModel
    {
        public int? RichmondGoals, RichmondBehinds, RichmondScore;
        public int? OppositionGoals, OppositionBehinds, OppositionScore;
        public readonly Game NextGame;
        public readonly Game LastGame;
        public readonly string Opposition;

        public RichmondViewModel(List<Game> games)
        {
            LastGame = games.OrderByDescending(x => x.round).First(x => x.complete > 0);
            NextGame = games.OrderBy(x => x.round).FirstOrDefault(x => x.complete < 100);
            var dateTime = DateTime.Parse(NextGame.date);
            LastGame.date = DateTime.Parse(LastGame.date).Humanize();
            if (LastGame.hteam == "Richmond")
            {
                (RichmondGoals, RichmondBehinds, RichmondScore) = (LastGame.hgoals, LastGame.hbehinds, LastGame.hscore);
                (OppositionGoals, OppositionBehinds, OppositionScore, Opposition) = (LastGame.agoals, LastGame.abehinds, LastGame.ascore, LastGame.ateam);
            }
            else
            {
                (RichmondGoals, RichmondBehinds, RichmondScore) = (LastGame.agoals, LastGame.abehinds, LastGame.ascore);
                (OppositionGoals, OppositionBehinds, OppositionScore, Opposition) = (LastGame.hgoals, LastGame.hbehinds, LastGame.hscore, LastGame.hteam);
            }
            NextGame.date = $"{dateTime:ddd MMM dd} at {dateTime:h:mm tt} ({dateTime.Humanize()})";
        }
    }
}
