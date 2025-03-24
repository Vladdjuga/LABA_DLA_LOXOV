using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02_DLL.Models
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Level { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
    }
}
