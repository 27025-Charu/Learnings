using System;
using System.Collections.Generic;
using System.Text;
using VehicleWash.Models;

namespace VehicleWash.Repository
{
    internal interface IVehicleRecordRepository
    {
        void Add(VehicleRecord record);
        List<VehicleRecord> GetRecords(string vehicleNumber);
        List<VehicleRecord> GetAllRecords();
    }
}
