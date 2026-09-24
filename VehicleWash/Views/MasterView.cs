using System;
using VehicleWash.Services;

namespace VehicleWash.Views
{
    internal class MasterView
    {
        private readonly UserView _userView;
        private readonly VehicleView _vehicleView;
        private readonly VehicleWashView _vehicleWashView;

        public MasterView(UserView userView, VehicleView vehicleView, VehicleWashView vehicleWashView)
        {
            _userView = userView;
            _vehicleView = vehicleView;
            _vehicleWashView = vehicleWashView;
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                ConsoleHelper.PrintHeader("VEHICLE WASH - MAIN MENU");

                Console.WriteLine($@"
1. User authentication
2. Vehicle based CRUD operations
3. Vehicle Wash
4. Exit
");
                Console.Write("Enter choice: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _userView.ShowMenu();
                        break;
                    case "2":
                        _vehicleView.ShowMenu();
                        break;
                    case "3":
                        _vehicleWashView.ShowMenu();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}