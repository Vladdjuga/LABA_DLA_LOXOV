using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02_DLL.Models
{
    public class MatchPlayer
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Match Match { get; set; }
        public int? MatchId { get; set; }
        public Player Player { get; set; }
        public int? PlayerId { get; set; }
        public List<MatchEvent> MatchEvents { get; set; }
        public Position Position { get; set; }
    }
}
