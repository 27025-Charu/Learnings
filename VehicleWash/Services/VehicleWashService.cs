using System;
using System.Collections.Generic;
using System.Linq;
using VehicleWash.Models;
using VehicleWash.Repository;

namespace VehicleWash.Services
{
    internal class VehicleWashService
    {
        private readonly VehicleRepository vehicleRepository;
        private readonly VehicleRecordRepository recordRepository;
        private static readonly object SyncRoot = new object();
        private static readonly Queue<Vehicle> VehicleQueue = new Queue<Vehicle>();
        private static readonly List<Vehicle> WashingVehicles = new List<Vehicle>();
        public static int AvailableSlots { get; private set; } = 3;
        public event Action<Vehicle, Guid> WashCompleted;

        public VehicleWashService(VehicleRepository vehicleRepo, VehicleRecordRepository recordRepo)
        {
            vehicleRepository = vehicleRepo;
            recordRepository = recordRepo;
        }

        public bool StartWash(string vehicleNumber, Guid userId)
        {
            Vehicle vehicle = vehicleRepository.GetVehicleDetails(vehicleNumber);
            if (vehicle == null)
            {
                return false;
            }

            lock (SyncRoot)
            {
                if (vehicle.Status == VehicleStatus.InWash || vehicle.Status == VehicleStatus.InQueue)
                {
                    return false;
                }

                if (AvailableSlots > 0)
                {
                    StartVehicleWash(vehicle, userId);
                }
                else
                {
                    AddToQueue(vehicle, userId);
                }
            }

            return true;
        }

        private void StartVehicleWash(Vehicle vehicle, Guid userId)
        {
            vehicle.Status = VehicleStatus.InWash;
            WashingVehicles.Add(vehicle);
            AvailableSlots--;
            vehicleRepository.Update(vehicle, userId);
            StartTimer(vehicle, userId);
        }

        private void AddToQueue(Vehicle vehicle, Guid userId)
        {
            vehicle.Status = VehicleStatus.InQueue;
            VehicleQueue.Enqueue(vehicle);
            vehicleRepository.Update(vehicle, userId);
        }

        private void StartTimer(Vehicle vehicle, Guid userId)
        {
            System.Timers.Timer timer = new System.Timers.Timer(30_000);
            timer.AutoReset = false;
            timer.Elapsed += (sender, e) =>
            {
                CompleteWash(vehicle, timer, userId);
            };
            timer.Start();
        }

        private void CompleteWash(Vehicle vehicle, System.Timers.Timer timer, Guid userId)
        {
            timer.Stop();
            timer.Dispose();

            DateTime outTime = DateTime.Now;

            lock (SyncRoot)
            {
                WashingVehicles.Remove(vehicle);
                AvailableSlots++;
                vehicle.Status = VehicleStatus.IsFree;
                vehicle.LastServiceTime = outTime;
                vehicleRepository.Update(vehicle, userId);
                VehicleRecord record = new VehicleRecord
                {
                    VehicleNumber = vehicle.Vehicle_number,
                    InTime = outTime.AddSeconds(-30),
                    OutTime = outTime
                };
                recordRepository.Add(record);
                StartNextVehicle(userId);
            }
            WashCompleted?.Invoke(vehicle, userId);
        }

        private void StartNextVehicle(Guid userId)
        {
            if (VehicleQueue.Count == 0 || AvailableSlots == 0)
            {
                return;
            }

            Vehicle nextVehicle = VehicleQueue.Dequeue();
            StartVehicleWash(nextVehicle, userId);
        }

        public List<Vehicle> GetWashingVehicles()
        {
            lock (SyncRoot)
            {
                return WashingVehicles.ToList();
            }
        }

        public List<Vehicle> GetQueuedVehicles()
        {
            lock (SyncRoot)
            {
                return VehicleQueue.ToList();
            }
        }

        public int GetAvailableSlots()
        {
            lock (SyncRoot)
            {
                return AvailableSlots;
            }
        }

        public List<VehicleRecord> GetVehicleHistory(string vehicleNumber)
        {
            return recordRepository.GetRecords(vehicleNumber);
        }
    }
}