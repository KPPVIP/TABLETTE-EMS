using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phone.Models
{
    public class UserESXDataModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public bool insurance { get; set; }
        public string dateofbirth { get; set; }
        public string sex { get; set; }
        public string picture { get; set; }
        public int job_grade { get; set; }
        public int adamMedic { get; set; }
        public string callsignMedic { get; set; }
        public string phone_number { get; set; }
        public string accounts { get; set; }
        public int onDutyMedic { get; set; }
        public bool onPanicMedic { get; set; }
        public int money { get; set; }
        public int bank { get; set; }
    }
    public class Accounts
    {
        public int bank { get; set; }
    }
    public class UserDataModel
    {
        public int id { get; set; }
        public bool isActive { get; set; }
        public string balance { get; set; }
        public string age { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public bool insurance { get; set; }
        public string picture { get; set; }
        public int status { get; set; }
        public string registered { get; set; }
    }
}
