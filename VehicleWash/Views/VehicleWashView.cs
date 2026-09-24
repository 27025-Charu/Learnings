using System;
using System.Collections.Generic;
using System.Linq;
using VehicleWash.Models;
using VehicleWash.Services;

namespace VehicleWash.Views
{
    internal class VehicleWashView
    {
        private readonly VehicleWashService washService;
        public VehicleWashView(VehicleWashService washService)
        {
            this.washService = washService;
            this.washService.WashCompleted += HandleWashCompleted;
        }

        private void HandleWashCompleted(Vehicle vehicle, Guid userId)
        {
            if (userId == UserView.CurrentUserId)
            {
                Console.WriteLine($"Wash completed for vehicle {vehicle.Vehicle_number}.");
            }
        }

        public void ShowMenu()
        {
            if (UserView.CurrentUserId == Guid.Empty)
            {
                Console.WriteLine("You must be logged in to use the wash service.");
                return;
            }

            while (true)
            {
                List<Vehicle> washing = washService.GetWashingVehicles();
                List<Vehicle> queued = washService.GetQueuedVehicles();
                int slots = washService.GetAvailableSlots();
                string washingList = washing.Count == 0? "None": string.Join(", ", washing.Select(v => $"Vehicle Number: {v.Vehicle_number} (Vehicle Make: {v.make}, Vehicle Model: {v.model})"));
                string queueList = queued.Count == 0 ? "None": string.Join(", ", queued.Select((v, i) => $"{i + 1}. Vehicle Number: {v.Vehicle_number} (Vehicle Make: {v.make}, Vehicle Model: {v.model})"));
                ConsoleHelper.PrintHeader("VEHICLE WASH");
                Console.WriteLine($@"
Available Slots : {slots}
Washing Now     : {washingList}
Queue           : {queueList}
-------------------------
1. Start Wash
2. Vehicle Service History
3. Back
");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        StartWash();
                        break;
                    case "2":
                        ShowHistory();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void StartWash()
        {
            Console.Write("Vehicle Number: ");
            string vehicleNumber = Console.ReadLine();
            bool result = washService.StartWash(vehicleNumber, UserView.CurrentUserId);
            Console.WriteLine(result ? $"Wash started for {vehicleNumber}." : "Unable to start wash.");
        }

        private void ShowHistory()
        {
            Console.Write("Enter Vehicle Number: ");
            string vehicleNumber = Console.ReadLine();
            List<VehicleRecord> records = washService.GetVehicleHistory(vehicleNumber);
            if (records.Count == 0)
            {
                Console.WriteLine("No service history found.");
                return;
            }

            string recordhistory = string.Join("\n", records.Select(r => $"{r.VehicleNumber} | In: {r.InTime} | Out: {r.OutTime}"));
            Console.WriteLine($@"
===== SERVICE HISTORY =====
{recordhistory}
");
        }
    }
}