using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Interface;
using ExpenseTracker.Model;
using ExpenseTracker.Services;

namespace ExpenseTracker.View
{
    /// <summary>
    /// Provides methods for displaying finance-related menus and messages to the console.
    /// </summary>
    internal class FinanceView
    {
        private Helper _helper = new ();
        private FinanceService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceView"/> class.
        /// Initializes the <see cref="FinanceView"/> class.
        /// </summary>
        /// <param name="service">The Finance service that needs to be used.</param>
        public FinanceView(FinanceService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Displays the main menu options to the console.
        /// </summary>
        internal static void DisplayMainMenu()
        {
            Console.WriteLine($@"========================
MAIN MENU
========================
[{(int)MainMenu.IncomeMenu}]. Income Menu
[{(int)MainMenu.ExpenseMenu}]. Expense Menu
[{(int)MainMenu.ShowSummary}]. Show Summary (Net balance)
[{(int)MainMenu.ShowSummaryPerUser}]. Show Summary for a single user (Net balance) 
[{(int)MainMenu.Exit}]. Exit
========================
");
        }

        /// <summary>
        /// Displays the expense menu options to the console.
        /// </summary>
        internal static void DisplayExpenseMenu()
        {
            Console.WriteLine($@"========================
EXPENSE MENU
========================
[{(int)ExpenseMenu.AddExpense}]. Add expense
[{(int)ExpenseMenu.ViewExpenses}]. View expenses
[{(int)ExpenseMenu.EditExpense}]. Edit expense
[{(int)ExpenseMenu.DeleteExpense}]. Delete expense
[{(int)ExpenseMenu.Exit}]. Exit
========================
");
        }

        /// <summary>
        /// Displays the income menu options to the console.
        /// </summary>
        internal static void DisplayIncomeMenu()
        {
            Console.WriteLine($@"========================
INCOME MENU
========================
[{(int)IncomeMenu.AddIncome}]. Add income
[{(int)IncomeMenu.ViewIncomes}]. View incomes
[{(int)IncomeMenu.EditIncome}]. Edit income
[{(int)IncomeMenu.DeleteIncome}]. Delete income
[{(int)IncomeMenu.Exit}]. Exit
========================
");
        }

        /// <summary>
        /// Displays the application header in the console.
        /// </summary>
        internal static void DisplayHeader()
        {
            int width = Console.WindowWidth;
            Console.WriteLine(new string('=', width));

            string[] title =
{
        "███████ ██   ██ ██████  ███████ ███    ██ ███████ ███████     ████████ ██████   █████   ██████ ██   ██ ███████ ██████  ",
        "██       ██ ██  ██   ██ ██      ████   ██ ██      ██             ██    ██   ██ ██   ██ ██      ██  ██  ██      ██   ██ ",
        "█████     ███   ██████  █████   ██ ██  ██ ███████ █████          ██    ██████  ███████ ██      █████   █████   ██████  ",
        "██       ██ ██  ██      ██      ██  ██ ██      ██ ██             ██    ██   ██ ██   ██ ██      ██  ██  ██      ██   ██ ",
        "███████ ██   ██ ██      ███████ ██   ████ ███████ ███████        ██    ██   ██ ██   ██  ██████ ██   ██ ███████ ██   ██ ",
};
            foreach (string line in title)
            {
                int leftpadding = (width - line.Length) / 2;
                if (leftpadding < 0)
                {
                    leftpadding = 0;
                }

                Console.Write(new string(' ', leftpadding));
                Console.WriteLine(line);
            }

            Console.WriteLine(new string('=', width));
        }

        /// <summary>
        /// Displays a summary of total income, total expense, and net balance to the console.
        /// </summary>
        /// <param name="totalIncome">The total income amount.</param>
        /// <param name="totalExpense">The total expense amount.</param>
        internal static void DisplaySummary(decimal totalIncome, decimal totalExpense)
        {
            Console.OutputEncoding = Encoding.UTF8;
            decimal total = totalIncome - totalExpense;
            Console.WriteLine($@"========================
SUMMARY
========================
Total Income : {totalIncome:C}
Total Expense: {totalExpense:C}
Net Balance  : {total:C}
        ");
            if (total < 0)
            {
                // Use Helper as static for error display in static context
                new Helper().DisplayError("The Net Balance calculated is negative.");
            }
        }

        /// <summary>
        /// Displaying the specific transaction.
        /// </summary>
        /// <param name="transaction">Transaction - Income/ Expense </param>
        internal static void DisplayTransaction(ITransaction transaction)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($@"========================
Id         :{transaction.Id}
Amount     :{transaction.Amount}
Date       :transaction.Date
Category   :transaction.Category
========================
");
        }

        /// <summary>
        /// Displays a summary of total income, total expense, and net balance for a user to the console.
        /// </summary>
        /// <param name="totalIncome">The total income amount for the user.</param>
        /// <param name="totalExpense">The total expense amount for the user.</param>
        /// <param name="balance">The net balance for the user.</param>
        internal static void DisplayUserSummary(decimal totalIncome, decimal totalExpense, decimal balance)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($@"========================
USER SUMMARY
========================
Total Income: {totalIncome:C}
Total Expense: {totalExpense:C}
Net Balance: {balance:C}
");
        }

        /// <summary>
        /// Reads the user's menu choice from the console.
        /// </summary>
        /// <returns>The integer value of the user's choice.</returns>
        internal static int ReadChoice()
        {
            Console.WriteLine("Enter your choice: ");
            var input = Console.ReadLine();
            return int.TryParse(input, out var choice) ? choice : -1;
        }

        /// <summary>
        /// Reads a 3-digit numeric ID part from the user input.
        /// Prompts the user up to three times if the input is null.
        /// </summary>
        /// <returns>
        /// The 3-digit numeric string entered by the user, or null if all attempts fail.
        /// </returns>
        internal static string? ReadId()
        {
            Console.WriteLine("Please enter only the 3-digit numeric part of the ID numbers (e.g., enter 001 for both INC001 and EXP001): ");
            string? input = null;
            var helper = new Helper();
            for (int attempts = 0; attempts < 3; attempts++)
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    helper.DisplayError("Enter a valid ID. The input is null or empty.");
                    continue;
                }
                else
                {
                    return input;
                }
            }

            return null;
        }
    }
}
