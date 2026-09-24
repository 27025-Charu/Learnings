using System;

namespace VehicleWash.Models
{
    public class Vehicle
    {
        public string Vehicle_number { get; set; }
        public Guid Owner { get; set; }
        public DateTime LastServiceTime { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public VehicleStatus Status { get; set; }
    }
}