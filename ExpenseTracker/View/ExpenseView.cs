using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExpenseTracker.Model;
using ExpenseTracker.Services;
using Spectre.Console;

namespace ExpenseTracker.View
{
    /// <summary>
    /// Provides methods for reading and displaying expense details in the console.
    /// </summary>
    public class ExpenseView
    {
        private const int RETRYLIMIT = 3;
        private static readonly Helper _helper = new ();
        private static FinanceService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseView"/> class.
        /// </summary>
        /// <param name="service">The finance service instance to use.</param>
        public ExpenseView(FinanceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Initializes the static <see cref="FinanceService"/> instance for use in static methods.
        /// </summary>
        /// <param name="service">The finance service instance to initialize with.</param>
        public static void Initialize(FinanceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Reads the details of an expense from user input and adds or updates the expense in the repository.
        /// </summary>
        /// <param name="includeId">If <c>true</c>, prompts for a new expense ID; otherwise, uses the existing ID.</param>
        /// <param name="type">The transaction type (should be <see cref="TransactionType.Expense"/>).</param>
        /// <param name="existingId">The existing expense ID to use when <paramref name="includeId"/> is <c>false</c>.</param>
        /// <returns>
        /// <c>true</c> if the expense details were successfully read and processed; otherwise, <c>false</c>.
        /// </returns>
        public static bool ReadExpenseDetails(bool includeId, TransactionType type, string? existingId = null)
        {
            Expense expense = new ();
            bool isExistingRecord = false;

            if (includeId)
            {
                string pattern = @"^EXP(?!000)\d{3}$";
                for (int attempt = 1; attempt <= RETRYLIMIT; attempt++)
                {
                    Console.WriteLine($@"Enter the Expense Details:
---------------------
Enter the Id (Format : EXP001, EXP002): ");
                    string? input = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(input))
                    {
                        _helper.DisplayError("Error: ID cannot be empty.");
                    }
                    else if (Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
                    {
                        string id = input.ToUpper();
                        if (_service.Exists(id, type))
                        {
                            _helper.DisplayError($"The ID '{id}' already exists in the repository.");
                            Console.Write("Do you want to append new category details to this existing record? [y/n]: ");
                            string? userChoice = Console.ReadLine()?.Trim().ToLower();
                            if (userChoice != "y" && userChoice != "yes")
                            {
                                Console.WriteLine("Operation cancelled. Returning to Expense menu.");
                                return true;
                            }

                            var existingExpense = _service.GetAll(type).OfType<Expense>().FirstOrDefault(x => x.Id == id);
                            if (existingExpense != null)
                            {
                                expense = existingExpense;
                                isExistingRecord = true;
                                _helper.DisplaySuccess("Ready to append new entries.");
                                break;
                            }
                        }

                        expense.Id = id;
                        _helper.DisplaySuccess("Valid ID format.");
                        break;
                    }
                    else
                    {
                        _helper.DisplayError("Invalid ID format. Expected: EXP followed by 3 digits (e.g, EXP001).");
                    }

                    if (attempt == RETRYLIMIT)
                    {
                        _helper.ShowRetryMessage("Maximum attempts reached. Exiting operation.");
                        return false;
                    }
                }
            }
            else
            {
                string id = existingId?.ToUpper() ?? string.Empty;
                var existingExpense = _service.GetAll(type).OfType<Expense>().FirstOrDefault(x => x.Id == id);
                if (existingExpense == null)
                {
                    _helper.DisplayError($"Error: The ID '{id}' does not exist in the repository.");
                    return false;
                }

                expense = existingExpense;
                isExistingRecord = true;
            }

            bool addExpense = true;
            string[] allowedFormats = { "d/M/yyyy", "d/M/yy", "dd/MM/yyyy" };

            while (addExpense)
            {
                ExpenseCategory selectedCategory = default;
                decimal selectedAmount = 0;
                DateOnly entryDate = DateOnly.FromDateTime(DateTime.Now);

                Console.WriteLine($@"---------------------
Expense Categories:
---------------------");

                foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
                {
                    Console.WriteLine($"{(int)category}. {category}");
                }

                bool categorySuccess = false;
                for (int attempt = 1; attempt <= RETRYLIMIT; attempt++)
                {
                    Console.Write($"Attempt {attempt}/{RETRYLIMIT} - Select Category (enter number): ");
                    string? input = Console.ReadLine()?.Trim();
                    if (int.TryParse(input, out int categoryNumber) && Enum.IsDefined(typeof(ExpenseCategory), categoryNumber))
                    {
                        selectedCategory = (ExpenseCategory)categoryNumber;
                        categorySuccess = true;
                        break;
                    }

                    _helper.DisplayError($"Invalid selection. Please enter a valid category number.");
                    if (attempt == RETRYLIMIT)
                    {
                        return false;
                    }
                }

                if (!categorySuccess)
                {
                    return false;
                }

                bool amountSuccess = false;
                for (int attempt = 1; attempt <= RETRYLIMIT; attempt++)
                {
                    if (Helper.TryReadDecimal($@"Attempt {attempt}/{RETRYLIMIT} - Enter the Amount: ", out selectedAmount))
                    {
                        if (Helper.IsValidDecimal(selectedAmount, out string? amountError))
                        {
                            selectedAmount = Math.Round(selectedAmount, 2);
                            amountSuccess = true;
                            break;
                        }

                        _helper.DisplayError(amountError ?? "Invalid amount range.");
                    }
                    else
                    {
                        _helper.DisplayError("Invalid number. Please enter a valid price decimal.");
                    }

                    if (attempt == RETRYLIMIT)
                    {
                        return false;
                    }
                }

                if (!amountSuccess)
                {
                    return false;
                }

                bool dateSuccess = false;
                for (int attempts = 1; attempts <= RETRYLIMIT; attempts++)
                {
                    Console.Write($"Attempt {attempts}/{RETRYLIMIT} - Enter Date for this item (Press Enter for Today's date): ");
                    string? dateInput = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(dateInput))
                    {
                        entryDate = DateOnly.FromDateTime(DateTime.Now);
                        dateSuccess = true;
                        break;
                    }

                    if (DateOnly.TryParseExact(dateInput, allowedFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateOnly parsedDate))
                    {
                        entryDate = parsedDate;
                        dateSuccess = true;
                        break;
                    }

                    _helper.DisplayError("Invalid date format. Expected layout: dd/MM/yyyy.");
                    if (attempts == RETRYLIMIT)
                    {
                        return false;
                    }
                }

                if (!dateSuccess)
                {
                    return false;
                }

                var existingEntry = expense.Entries.FirstOrDefault(e => e.Category == selectedCategory && e.Date == entryDate);
                if (existingEntry != null)
                {
                    existingEntry.Amount += selectedAmount;
                }
                else
                {
                    expense.Entries.Add(new ExpenseCategoryAmount
                    {
                        Category = selectedCategory,
                        Amount = selectedAmount,
                        Date = entryDate,
                    });
                }

                Console.Write("Do you want to add the expense for some other category? [Y/N]: ");
                string loopChoice = Console.ReadLine()?.Trim().ToUpper() ?? "N";
                if (loopChoice == "N" || loopChoice == "NO")
                {
                    addExpense = false;
                }
            }

            if (includeId && !isExistingRecord)
            {
                _service.Add(expense, type);
                _helper.DisplaySuccess("New record added successfully!");
            }
            else
            {
                bool flag = _service.Update(expense.Id, expense);
                if (!flag)
                {
                    _helper.DisplayError("Failed to update the repository database.");
                    return false;
                }

                _helper.DisplaySuccess("Data successfully appended to the record!");
            }

            return true;
        }

        /// <summary>
        /// Displays all expenses by retrieving them from the service and formatting them in a table.
        /// </summary>
        public static void DisplayAllExpenses()
        {
            var expensesList = _service.GetAll(TransactionType.Expense).OfType<Expense>().ToList();
            DisplayListOfExpenses(expensesList);
        }

        /// <summary>
        /// Deletes an expense with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the expense to delete.</param>
        /// <returns><c>true</c> if the expense was deleted; otherwise, <c>false</c>.</returns>
        public static bool DeleteExpense(string id)
        {
            var transaction = _service.GetById(id.ToUpper(), TransactionType.Expense) as Expense;
            if (transaction == null)
            {
                _helper.DisplayError("There is no transaction-Expense with that Id.");
                return false;
            }

            FinanceView.DisplayTransaction(transaction);
            Console.WriteLine(@"Deletion Menu:
1. Delete the ENTIRE transaction file
2. Delete a SPECIFIC category entry from this file
3. Cancel the deletion operation itself");
            Console.Write("Select an option (1-3): ");
            string? choice = Console.ReadLine()?.Trim();
            if (choice == "1")
            {
                Console.Write("Are you absolutely sure you want to delete this ENTIRE Expense record? (y/n): ");
                string? confirm = Console.ReadLine()?.Trim().ToLower();
                if (confirm == "y" || confirm == "yes")
                {
                    _service.Delete(id.ToUpper(), TransactionType.Expense);
                    _helper.DisplaySuccess("The entire expense record was deleted.");
                    return true;
                }
            }
            else if (choice == "2")
            {
                if (transaction.Entries.Count == 0)
                {
                    _helper.DisplayError("This transaction file contains 0 entries.");
                    return true;
                }

                Console.WriteLine("\nSelect the entry line to remove:");
                for (int i = 0; i < transaction.Entries.Count; i++)
                {
                    var entry = transaction.Entries[i];
                    Console.WriteLine($"{i + 1}. Category: {entry.Category} , Date: {entry.Date:dd/MM/yyyy} , Amount: ₹ {entry.Amount:N2}");
                }

                Console.Write("Enter the number of the item to delete: ");
                if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= transaction.Entries.Count)
                {
                    var targetEntry = transaction.Entries[index - 1];
                    transaction.Entries.Remove(targetEntry);
                    _service.Update(transaction.Id, transaction);
                    _helper.DisplaySuccess($"Successfully removed the {targetEntry.Category} entry.");
                    if (transaction.Entries.Count == 0)
                    {
                        _service.Delete(transaction.Id, TransactionType.Expense);
                        _helper.DisplayError("The file became empty and was removed from the repository.");
                    }

                    return true;
                }
                else
                {
                    _helper.DisplayError("Invalid numerical index selected. Deletion aborted.");
                }
            }

            Console.WriteLine("Operation cancelled. Returning to main operations interface.");
            return true;
        }

        /// <summary>
        /// Displays a list of expenses in a formatted table.
        /// </summary>
        /// <param name="expenses">The list of <see cref="Expense"/> objects to display.</param>
        public static void DisplayListOfExpenses(List<Expense> expenses)
        {
            if (expenses == null || expenses.Count == 0)
            {
                _helper.DisplayError("There is no expenses stored in the repository.");
                return;
            }

            Table tab = new ();
            tab.AddColumn(new TableColumn("[blue]ID[/]").Centered());
            tab.AddColumn(new TableColumn("[blue]Category & Date Breakdown[/]").LeftAligned());
            tab.AddColumn(new TableColumn("[blue]Amount[/]").RightAligned());

            Console.OutputEncoding = Encoding.UTF8;

            foreach (var expense in expenses)
            {
                string categoriesWithDates = string.Join(
                    Environment.NewLine,
                    expense.Entries.Select(e => $"• {e.Category} ([grey]{e.Date:dd/MM/yyyy}[/])"));

                string amounts = string.Join(
                    Environment.NewLine,
                    expense.Entries.Select(e => $"₹ {e.Amount:N2}"));
                amounts += $"{Environment.NewLine}[grey]----------[/]{Environment.NewLine}[bold]₹ {expense.Amount:N2}[/]";
                tab.AddRow(
                    $"[yellow]{expense.Id}[/]",
                    categoriesWithDates,
                    amounts);
            }

            AnsiConsole.Write(tab);
            Console.WriteLine($"Total transaction files displayed: {expenses.Count}");
        }

        /// <summary>
        /// Reads the ID of the expense from user input.
        /// </summary>
        /// <returns>
        /// The entered expense ID as a string, or an empty string if input is null.
        /// </returns>
        public static string? ReadId()
        {
            Console.WriteLine("Enter the Id of the expense: ");
            string? input = Console.ReadLine();
            return input ?? string.Empty;
        }
    }
}
