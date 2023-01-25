using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phone.Models
{
    public class AmbulanceOnDutyModel
    {
        public string name { get; set; } 
        public int rank { get; set; }
        public string callsign { get; set; }
        public int medicId { get; set; }
        public int onDuty { get; set; }
        public int adam { get; set; }
        public bool onPanic { get; set; }
    }
}
