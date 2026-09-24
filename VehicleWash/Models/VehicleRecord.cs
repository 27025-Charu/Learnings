using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleWash.Models
{
    public class VehicleRecord
    {
        public string VehicleNumber { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
    }
}
