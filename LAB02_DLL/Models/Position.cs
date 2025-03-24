using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02_DLL.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Player>? Players { get; set; }
        public List<MatchPlayer>? MatchPlayers { get; set; }
    }
}
