using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02_DLL.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        public Team Team { get; set; }
        public int? TeamId { get; set; }
        public List<Position> Positions { get; set; }
        public List<MatchPlayer>? MatchPlayers { get; set; }
    }
}
