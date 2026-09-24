using System;

namespace VehicleWash.Views
{
    internal static class ConsoleHelper
    {
        public static void PrintHeader(string title)
        {
            int width;
            try
            {
                width = Console.WindowWidth > 0 ? Console.WindowWidth : 80;
            }
            catch
            {
                width = 80;
            }

            string username = string.IsNullOrEmpty(UserView.CurrentUserName) ? "Guest" : UserView.CurrentUserName;
            string right = $"User: {username}";
            int padding = width - title.Length - right.Length;
            if (padding < 1)
            {
                padding = 1;
            }

            Console.WriteLine(new string('=', width));
            Console.WriteLine(title + new string(' ', padding) + right);
            Console.WriteLine(new string('=', width));
        }
    }
}