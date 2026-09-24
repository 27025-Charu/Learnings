using System;
using VehicleWash.Models;
using VehicleWash.Services;

namespace VehicleWash.Views
{
    internal class UserView
    {
        private readonly UserAuthService userAuthService;
        public static Guid CurrentUserId { get; private set; } = Guid.Empty;
        public static string CurrentUserName { get; private set; } = string.Empty;

        public UserView(UserAuthService userAuthService)
        {
            this.userAuthService = userAuthService;
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
5. Delete Account
6. Logout
7. Back
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
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();

            User user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                EmailId = email,
                password = password,
                phoneNumber = phoneNumber
            };

            bool result = userAuthService.Register(user);

            Console.WriteLine(result ? "Registration successful." : "Registration failed, email already in use.");
        }

        private bool Login()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            bool result = userAuthService.Login(password, email);

            if (result)
            {
                CurrentUserId = userAuthService.GetId(email);
                User user = userAuthService.GetUser(CurrentUserId);
                CurrentUserName = user != null ? $"{user.FirstName} {user.LastName}" : email;
                Console.WriteLine($"Login successful. Welcome, {CurrentUserName}.");
                return true;
            }

            Console.WriteLine("Invalid email or password.");
            return false;
        }

        private void ViewProfile()
        {
            if (CurrentUserId == Guid.Empty)
            {
                Console.WriteLine("You must be logged in.");
                return;
            }

            User user = userAuthService.GetUser(CurrentUserId);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine($@"
Name : {user.FirstName} {user.LastName}
Email: {user.EmailId}
Phone: {user.phoneNumber}
");
        }

        private void UpdateProfile()
        {
            if (CurrentUserId == Guid.Empty)
            {
                Console.WriteLine("You must be logged in.");
                return;
            }

            User user = userAuthService.GetUser(CurrentUserId);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.Write("First Name: ");
            user.FirstName = Console.ReadLine();

            Console.Write("Last Name: ");
            user.LastName = Console.ReadLine();

            Console.Write("Phone Number: ");
            user.phoneNumber = Console.ReadLine();

            bool result = userAuthService.UpdateUser(user);

            if (result)
            {
                CurrentUserName = $"{user.FirstName} {user.LastName}";
            }

            Console.WriteLine(result ? "Profile updated." : "Update failed.");
        }

        private void DeleteAccount()
        {
            if (CurrentUserId == Guid.Empty)
            {
                Console.WriteLine("You must be logged in.");
                return;
            }

            bool result = userAuthService.DeleteUser(CurrentUserId);

            if (result)
            {
                CurrentUserId = Guid.Empty;
                CurrentUserName = string.Empty;
                Console.WriteLine("Account deleted.");
            }
            else
            {
                Console.WriteLine("Delete failed.");
            }
        }

        private void Logout()
        {
            CurrentUserId = Guid.Empty;
            CurrentUserName = string.Empty;
            Console.WriteLine("Logged out.");
        }
    }
}