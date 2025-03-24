using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02_DLL.Models
{
    public class EventType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MatchEvent> MatchEvents { get; set; }
    }
}
