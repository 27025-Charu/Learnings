using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ExpenseTracker.Interface;
using ExpenseTracker.Model;
using Spectre.Console;

namespace ExpenseTracker.Services
{
    /// <summary>
    /// Provides helper methods for displaying messages and errors to the console.
    /// </summary>
    internal class Helper
    {
        /// <summary>
        /// Tries to read a decimal value from the console with the specified prompt.
        /// </summary>
        /// <param name="prompt">The prompt message to display to the user.</param>
        /// <param name="price">When this method returns, contains the decimal value entered by the user if the conversion succeeded, or zero if the conversion failed.</param>
        /// <returns>
        /// <c>true</c> if the user input was successfully parsed to a decimal value; otherwise, <c>false</c>.
        /// </returns>
        public static bool TryReadDecimal(string prompt, out decimal price)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                price = 0;
                return false;
            }

            return decimal.TryParse(input, out price);
        }

        /// <summary>
        /// Displays an error message to the console.
        /// </summary>
        /// <param name="errorMessage">The error message to display.</param>
        public void DisplayError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{errorMessage}");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays an success message to the console.
        /// </summary>
        /// <param name="successMessage">The success message to display.</param>
        public void DisplaySuccess(string successMessage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Success: {successMessage}");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays a retry message to the user in magenta text, indicating an error and prompting for retry.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void ShowRetryMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Validates that the specified decimal amount is within the allowed range.
        /// </summary>
        /// <param name="amount">The decimal amount to validate.</param>
        /// <param name="amountError">When this method returns, contains the error message if validation fails; otherwise, an empty string.</param>
        /// <returns>
        /// <c>true</c> if the amount is valid; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValidDecimal(decimal amount, out string amountError)
        {
            amountError = string.Empty;
            if (amount <= 0)
            {
                amountError = "Amount can't be a negative number.";
                return false;
            }

            if (amount > 1000000)
            {
                amountError = "Amount can't exceed 1000000";
                return false;
            }

            return true;
        }
    }
}
