using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phone.Models
{
    public class AmbulanceCallsModel
    {
        public int id { get; set; }
        public string from { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public string crew { get; set; }
        public string message { get; set; }
    }

    public class AmbulanceCallsCadModel
    {
        public int id { get; set; }
        public string from { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public List<string> crew { get; set; }
        public string message { get; set; }
    }

}
