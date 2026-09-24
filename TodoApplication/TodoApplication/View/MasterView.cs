using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Model;

namespace TodoApplication.View
{
    internal class MasterView
    {
        private readonly UserView _userView;
        private readonly TaskView _taskView;

        public MasterView(UserView userView, TaskView taskView)
        {
            _userView = userView;
            _taskView = taskView;
        }

        public static StorageType ReadStorageChoice()
        {
            Console.Write("Enter your storage choice: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return StorageType.Invalid;
            }

            return ParseEnumChoice<StorageType>(input);
        }
        private static T ParseEnumChoice<T>(string input)
            where T : struct, Enum
        {
            if (int.TryParse(input, out int numericChoice))
            {
                if (Enum.IsDefined(typeof(T), numericChoice))
                {
                    return (T)(object)numericChoice;
                }
            }

            return default(T);
        }
        internal static void RepoStorageMenu()
        {
            Console.WriteLine($@"---- REPOSITORY STORAGE OPTIONS ----
{(int)StorageType.InMemory}. In-Memory Storage
{(int)StorageType.Json}. JSON File Storage
{(int)StorageType.Invalid}. Invalid Option
");
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                ConsoleHelper.PrintHeader("TODO APPLICATION - MAIN MENU");

                Console.WriteLine($@"
1. User authentication
2. Task based CRUD operations
3. Exit
");
                Console.Write("Enter choice: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        _userView.ShowMenu();
                        break;
                    case "2":
                        _taskView.ShowMenu();
                        break;
                    case "3":
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
