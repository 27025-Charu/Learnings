using System;
using System.Collections.Generic;
using System.Linq;
using VehicleWash.Models;

namespace VehicleWash.Repository
{
    internal class VehicleRepository : IVehicle
    {
        private List<Vehicle> vehicles = new List<Vehicle>();

        public void Add(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }

        public bool Delete(string vehicleNumber)
        {
            Vehicle vehicle = GetVehicleDetails(vehicleNumber);
            if (vehicle == null)
            {
                return false;
            }
            vehicles.Remove(vehicle);
            return true;
        }

        public Vehicle GetVehicleDetails(string vehicleNumber)
        {
            return vehicles.Find(v => v.Vehicle_number == vehicleNumber);
        }

        public List<Vehicle> GetVehiclesByOwner(Guid ownerId)
        {
            return vehicles.Where(v => v.Owner == ownerId).ToList();
        }

        public bool Update(Vehicle vehicle)
        {
            Vehicle existing = GetVehicleDetails(vehicle.Vehicle_number);
            if (existing == null)
            {
                return false;
            }

            existing.make = vehicle.make;
            existing.model = vehicle.model;
            existing.Owner = vehicle.Owner;
            existing.Status = vehicle.Status;
            existing.LastServiceTime = vehicle.LastServiceTime;

            return true;
        }

        public bool Update(Vehicle vehicle, Guid userId)
        {
            return Update(vehicle);
        }
    }
}