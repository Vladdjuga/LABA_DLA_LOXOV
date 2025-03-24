using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02_DLL.Models
{
    public class MatchEvent
    {
        public int Id { get; set; }
        public int Minute { get; set; }
        public Match Match { get; set; }
        public int? MatchId { get; set; }
        public EventType EventType { get; set; }
        public MatchPlayer? MatchPlayer { get; set; }
    }
}
