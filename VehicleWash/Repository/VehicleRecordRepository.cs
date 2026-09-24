using System;
using System.Collections.Generic;
using System.Text;
using VehicleWash.Models;

namespace VehicleWash.Repository
{
    internal class VehicleRecordRepository : IVehicleRecordRepository
    {
        private List<VehicleRecord> vehicleRecords = new List<VehicleRecord>();
        public void Add(VehicleRecord record)
        {
            vehicleRecords.Add(record);
        }

        public List<VehicleRecord> GetAllRecords()
        {
            return vehicleRecords;
        }

        public List<VehicleRecord> GetRecords(string vehicleNumber)
        {
            return vehicleRecords.FindAll(r => r.VehicleNumber == vehicleNumber);
        }
    }
}
