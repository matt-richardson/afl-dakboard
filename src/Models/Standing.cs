using System.Collections.Generic;

namespace afl_dakboard.Models
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Standing    {
        public int goals_for { get; set; }
        public int wins { get; set; }
        public int @for { get; set; }
        public string name { get; set; }
        public int played { get; set; }
        public int pts { get; set; }
        public int rank { get; set; }
        public double percentage { get; set; }
        public int goals_against { get; set; }
        public int id { get; set; }
        public int behinds_for { get; set; }
        public int against { get; set; }
        public int losses { get; set; }
        public int behinds_against { get; set; }
        public int draws { get; set; }
    }

    public class StandingsRoot    {
        public List<Standing> standings { get; set; }
    }
}
