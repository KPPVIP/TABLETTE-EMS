using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phone.Models
{
    public class VehicleDataModel
    {
        public string owner { get; set; }
        public string plate { get; set; }
        public string vehicle { get; set; }
        public string type { get; set; }
        public string job { get; set; }
        public bool stored { get; set; }
    }
    public class VehicleModel
    {
        public string plate { get; set; }
        public int model { get; set; }
    }
    public class VehicleCadModel
    {
        public int carId { get; set; }
        public string name { get; set; }
        public string ownerId { get; set; }
        public string type { get; set; }
        public string number { get; set; }
        public int status { get; set; }
        public string color { get; set; }
        public int sellerId { get; set; }
    }
}
