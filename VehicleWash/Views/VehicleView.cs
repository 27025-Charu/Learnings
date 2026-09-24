using System;
using System.Collections.Generic;
using System.Linq;
using VehicleWash.Models;
using VehicleWash.Services;

namespace VehicleWash.Views
{
    internal class VehicleView
    {
        private readonly VehicleService vehicleService;

        public VehicleView(VehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public void ShowMenu()
        {
            if (UserView.CurrentUserId == Guid.Empty)
            {
                Console.WriteLine("You must be logged in to manage vehicles.");
                return;
            }

            while (true)
            {
                List<Vehicle> myVehicles = vehicleService.GetVehiclesByOwner(UserView.CurrentUserId);

                string vehicleList = myVehicles.Count == 0
                    ? "No vehicles registered."
                    : string.Join("\n", myVehicles.Select(v => $"{v.Vehicle_number} - {v.make} {v.model} - {v.Status}"));

                ConsoleHelper.PrintHeader("MY VEHICLES");

                Console.WriteLine($@"
{vehicleList}
--------------------
1. Add Vehicle
2. Update Vehicle
3. Delete Vehicle
4. Back
");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddVehicle();
                        break;
                    case "2":
                        UpdateVehicle();
                        break;
                    case "3":
                        DeleteVehicle();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void AddVehicle()
        {
            Console.Write("Vehicle Number: ");
            string vehicleNumber = Console.ReadLine();

            Console.Write("Make: ");
            string make = Console.ReadLine();

            Console.Write("Model: ");
            string model = Console.ReadLine();

            Vehicle vehicle = new Vehicle
            {
                Vehicle_number = vehicleNumber,
                make = make,
                model = model,
                Owner = UserView.CurrentUserId,
                Status = VehicleStatus.IsFree
            };

            vehicleService.AddVehicle(vehicle);

            Console.WriteLine($"Vehicle {vehicleNumber} added successfully.");
        }

        private void UpdateVehicle()
        {
            Console.Write("Vehicle Number: ");
            string vehicleNumber = Console.ReadLine();

            Console.Write("New Make: ");
            string make = Console.ReadLine();

            Console.Write("New Model: ");
            string model = Console.ReadLine();

            Vehicle vehicle = new Vehicle
            {
                Vehicle_number = vehicleNumber,
                make = make,
                model = model,
                Owner = UserView.CurrentUserId,
                Status = VehicleStatus.IsFree
            };

            Console.Write("Email: ");
            string email = Console.ReadLine();

            bool result = vehicleService.UpdateVehicle(vehicle, email);

            Console.WriteLine(result ? $"Vehicle {vehicleNumber} updated successfully." : "Update failed.");
        }

        private void DeleteVehicle()
        {
            Console.Write("Vehicle Number: ");
            string vehicleNumber = Console.ReadLine();

            bool result = vehicleService.RemoveVehicle(vehicleNumber);

            Console.WriteLine(result ? $"Vehicle {vehicleNumber} deleted successfully." : "Vehicle not found.");
        }
    }
}