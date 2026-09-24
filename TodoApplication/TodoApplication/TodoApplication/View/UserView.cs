using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TodoApplication.Model;
using TodoApplication.Services;

namespace TodoApplication.View
{
    internal class UserView
    {
        private readonly UserAuthService _userAuthService;
        public static string CurrentUserId { get; set; }
        public static string CurrentUserName { get; private set; } = string.Empty;

        public UserView(UserAuthService userAuthService)
        {
            this._userAuthService = userAuthService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                ConsoleHelper.PrintHeader("USER MENU");

                Console.WriteLine($@"
1. Register
2. Login
3. View Profile
4. Update Profile
5. Delete User
6. Logout
7. Back [To access the Main menu and move to task]
");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        if (Login())
                        {
                            return;
                        }
                        break;
                    case "3":
                        ViewProfile();
                        break;
                    case "4":
                        UpdateProfile();
                        break;
                    case "5":
                        DeleteAccount();
                        break;
                    case "6":
                        Logout();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void RegisterUser()
        {
            int nameAttempts = 3;
            string name = string.Empty;
            string namePattern = @"^[A-Za-z\s]+$";

            while (nameAttempts > 0)
            {
                Console.Write("Enter Name: ");
                name = (Console.ReadLine() ?? string.Empty).Trim();

                if (!string.IsNullOrEmpty(name) && Regex.IsMatch(name, namePattern))
                {
                    break;
                }

                nameAttempts--;
                if (string.IsNullOrEmpty(name))
                {
                    Helper.DisplayError("Name can't be null or empty.");
                }
                else
                {
                    Helper.DisplayError("Name can contain only alphabets and spaces.");
                }

                if (nameAttempts > 0)
                {
                    Console.WriteLine($"Attempts remaining: {nameAttempts}");
                }
            }

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Registration aborted due to invalid Name.");
                return;
            }

            string pattern = @"^EMP(?!000)\d{3}$";
            int idAttempts = 3;
            string empId = string.Empty;
            while (idAttempts > 0)
            {
                Console.Write("Enter Employee Id [Format: EMP001,EMP002]: ");
                empId = (Console.ReadLine() ?? string.Empty).Trim();

                if (string.IsNullOrEmpty(empId))
                {
                    idAttempts--;
                    Helper.DisplayError("Error: ID cannot be empty.");
                    if (idAttempts > 0)
                    {
                        Console.WriteLine($"Attempts remaining: {idAttempts}");
                    }

                    continue;
                }

                if (!Regex.IsMatch(empId, pattern, RegexOptions.IgnoreCase))
                {
                    idAttempts--;
                    Helper.DisplayError("Error: Invalid Employee Id format.");
                    if (idAttempts > 0)
                    {
                        Console.WriteLine($"Attempts remaining: {idAttempts}");
                    }

                    continue;
                }

                empId = empId.ToUpper();
                break;
            }

            if (string.IsNullOrEmpty(empId))
            {
                Helper.DisplaySuccess("Registration aborted due to invalid Employee Id.");
                return;
            }

            int passAttempts = 3;
            string password = string.Empty;
            string passPattern = @"^[A-Za-z]{3}\d{5}$"; 

            while (passAttempts > 0)
            {
                Console.Write("Enter Password[Format: First three alphabets and five numbers(ABC00000)]: ");
                password = (Console.ReadLine() ?? string.Empty).Trim();

                if (string.IsNullOrEmpty(password))
                {
                    passAttempts--;
                    Helper.DisplayError("Password can't be null or empty.");
                    if (passAttempts > 0)
                    {
                        Console.WriteLine($"Attempts remaining: {passAttempts}");
                    }
                    continue;
                }

                if (!Regex.IsMatch(password, passPattern))
                {
                    passAttempts--;
                    Helper.DisplayError("Error: Invalid Password format. Expected: 3 letters followed by 5 digits (e.g. ABC00000).");
                    if (passAttempts > 0)
                    {
                        Console.WriteLine($"Attempts remaining: {passAttempts}");
                    }
                    continue;
                }

                break;
            }

            if (string.IsNullOrEmpty(password) || !Regex.IsMatch(password, passPattern))
            {
                Console.WriteLine("Registration aborted due to invalid Password.");
                return;
            }

            User user = new User
            {
                EmployeeId = empId,
                Name = name,
                Password = password,
            };

            bool result = _userAuthService.Register(user);
            Console.WriteLine(result ? "Registration successful." : "Registration failed, Id already in use.");
        }

        private bool Login()
        {
            string pattern = @"^EMP(?!000)\d{3}$";
            Console.Write("Enter Employee Id [Format: EMP001,EMP002]: ");
            string employeeId = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(employeeId))
            {
                Helper.DisplayError("Error: ID cannot be empty.");
                return false;
            }

            if (!Regex.IsMatch(employeeId, pattern, RegexOptions.IgnoreCase))
            {
                Helper.DisplayError("Error: Invalid Employee Id format.");
                return false;
            }

            string id = employeeId.ToUpper();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            bool result = _userAuthService.Login(password, id);

            if (result)
            {
                CurrentUserId = id;
                CurrentUserName = id;

                Helper.DisplaySuccess($"Login successful. Welcome, {CurrentUserName}.");
                return true;
            }

            Helper.DisplayError("Invalid Employee Id or Password.");
            return false;
        }

        private void ViewProfile()
        {
            if (CurrentUserId == null)
            {
                Console.WriteLine("You must be logged in.");
                return;
            }

            User user = _userAuthService.GetUser(CurrentUserId);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine($@"
Name : {user.Name},
Employee Id: {user.EmployeeId}
");
        }

        private void UpdateProfile()
        {
            if (CurrentUserId == null)
            {
                Console.WriteLine("You must be logged in.");
                return;
            }

            User user = _userAuthService.GetUser(CurrentUserId);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("Enter Name: ");
            user.Name = Console.ReadLine();

            bool result = _userAuthService.UpdateUser(user);

            if (result)
            {
                CurrentUserName = $"{user}";
            }

            Console.WriteLine(result ? "Profile updated." : "Update failed.");
        }

        private void DeleteAccount()
        {
            if (CurrentUserId == null)
            {
                Helper.DisplayError("You must be logged in.");
                return;
            }

            bool result = _userAuthService.DeleteUser(CurrentUserId);

            if (result)
            {
                CurrentUserId = string.Empty;
                CurrentUserName = string.Empty;
                Helper.DisplaySuccess("Account deleted.");
            }
            else
            {
                Helper.DisplayError("Delete failed.");
            }
        }

        private void Logout()
        {
            CurrentUserId = string.Empty;
            CurrentUserName = string.Empty;
            Helper.DisplaySuccess("Logged out.");
        }
    }
}
