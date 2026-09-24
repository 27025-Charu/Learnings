using System;
using System.Collections.Generic;
using VehicleWash.Models;

namespace VehicleWash.Repository
{
    internal interface IVehicle
    {
        void Add(Vehicle vehicle);
        bool Update(Vehicle vehicle);
        bool Delete(string vehicleNumber);
        Vehicle GetVehicleDetails(string vehicleNumber);
        List<Vehicle> GetVehiclesByOwner(Guid ownerId);
    }
}