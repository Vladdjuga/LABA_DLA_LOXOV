using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02_DLL.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Stadium { get; set; }
        public Team HomeTeam { get; set; }
        public int? HomeTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int? AwayTeamId { get; set; }
        public List<MatchEvent> MatchEvents { get; set; }
        public List<MatchPlayer> MatchPlayers { get; set; }
        public List<Article> Articles { get; set; }
    }
}
