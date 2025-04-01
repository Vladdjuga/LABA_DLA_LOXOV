using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02_DLL.Models
{
    public class DOTACharacter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<Player>? Players { get; set; }
        public int? PositionId { get; set; }
        public Position? Position { get; set; }
    }
}
