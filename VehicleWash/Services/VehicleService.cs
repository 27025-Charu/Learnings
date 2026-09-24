using System;
using System.Collections.Generic;
using VehicleWash.Models;
using VehicleWash.Repository;

namespace VehicleWash.Services
{
    internal class VehicleService
    {
        private readonly VehicleRepository _vehicleRepo;
        private readonly UserAuthService _userService;

        public VehicleService(VehicleRepository vehicleRepo, UserAuthService userService)
        {
            _vehicleRepo = vehicleRepo;
            _userService = userService;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            _vehicleRepo.Add(vehicle);
        }

        public bool RemoveVehicle(string vehicleNumber)
        {
            return _vehicleRepo.Delete(vehicleNumber);
        }

        public bool UpdateVehicle(Vehicle vehicle, string email)
        {
            var userId = _userService.GetId(email);
            if (userId == Guid.Empty)
            {
                return false;
            }
            return _vehicleRepo.Update(vehicle, userId);
        }

        public List<Vehicle> GetVehiclesByOwner(Guid ownerId)
        {
            return _vehicleRepo.GetVehiclesByOwner(ownerId);
        }
    }
}