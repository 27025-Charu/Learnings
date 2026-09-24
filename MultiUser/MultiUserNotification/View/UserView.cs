using MultiUserNotification.Model;
using MultiUserNotification.Services;

namespace MultiUserNotification.View
{
    internal class UserView
    {
        private Service _service;

        public UserView(Service service)
        {
            _service = service;
        }
        public void UserInputData()
        {
            Console.WriteLine(@"====================
USER 
====================
[L] LOGIN
[R] REGISTER
[E] EXIT");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.L:
                        Login();
                        break;
                    case ConsoleKey.R:
                        Register();
                        break;
                    case ConsoleKey.E:
                        Console.WriteLine("Exiting the application...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Enter a valid option. [L or R or E]");
                        continue;
                }
            }
        }

        private void Login()
        {
            int id = ReadIntegerInput("Enter the User Id:");
            Console.WriteLine("Enter the password:");
            string? password = Console.ReadLine();
            bool flag = _service.CheckLoginUser(id, password);
            if (flag)
            {
                Console.WriteLine("Login successful");
            }
            else
            {
                Console.WriteLine("Login unsuccessful");
            }
        }

        private void Register()
        {
            Console.WriteLine("Enter the User name:");
            string? name = Console.ReadLine();
            int id = ReadIntegerInput("Enter the User Id:");
            Console.WriteLine("Enter the Password:");
            string? password = Console.ReadLine();
            User user = new User()
            {
                UserName = name,
                UserId = id,
                Password = password,
            };
            _service.AddUser(user);
        }
        private int ReadIntegerInput(string prompt)
        {
            int result;
            while (true)
            {
                Console.WriteLine(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out result))
                {
                    return result;
                }

                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}
