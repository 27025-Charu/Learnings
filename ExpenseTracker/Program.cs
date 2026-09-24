using ExpenseTracker.Interface;
using ExpenseTracker.Model;
using ExpenseTracker.Repository;
using ExpenseTracker.Services;
using ExpenseTracker.View;
using Spectre.Console;

namespace Assignments
{
    /// <summary>
    /// Contains the entry point for the Expense Tracker application.
    /// </summary>
    internal class Program
    {
        private static readonly IRepository Repository = new FinanceRepository();
        private static readonly FinanceService Service = new FinanceService(Repository);

        private static readonly IncomeView IncomeView = new (Service);
        private static readonly ExpenseView ExpenseView = new (Service);
        private static readonly FinanceView CommonView = new (Service);
        private static readonly Helper _helper = new ();

        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        private static void Main()
        {
            FinanceView.DisplayHeader();
            bool flag = false;
            while (!flag)
            {
                FinanceView.DisplayMainMenu();
                var choice = FinanceView.ReadChoice();
                switch (choice)
                {
                    case 1:
                        RunIncomeMenu();
                        break;
                    case 2:
                        RunExpenseMenu();
                        break;
                    case 3:
                        ShowSummary();
                        break;
                    case 4:
                        ShowSummaryPerUser();
                        break;
                    case 5:
                        flag = true;
                        _helper.DisplaySuccess("Exiting the application...");
                        break;
                    default:
                        _helper.DisplayError("Invalid choice.");
                        break;
                }
            }

            static void RunExpenseMenu()
            {
                ExpenseView.Initialize(Service);
                bool back = false;
                while (!back)
                {
                    FinanceView.DisplayExpenseMenu();
                    switch (FinanceView.ReadChoice())
                    {
                        case 1:
                            bool flag = ExpenseView.ReadExpenseDetails(true, TransactionType.Expense);
                            if (flag)
                            {
                                _helper.DisplaySuccess("Expense added.");
                            }
                            else
                            {
                                _helper.DisplayError("Try again...");
                            }

                            PauseAndClear();
                            break;

                        case 2:
                            ExpenseView.DisplayAllExpenses();
                            PauseAndClear();
                            break;

                        case 3:
                            var id = ExpenseView.ReadId();
                            if (!string.IsNullOrEmpty(id))
                            {
                                flag = ExpenseView.ReadExpenseDetails(false, TransactionType.Expense, id);
                                if (flag)
                                {
                                    _helper.DisplaySuccess("Expense Updated.");
                                }
                                else
                                {
                                    _helper.DisplayError("Try again...");
                                }
                            }
                            else
                            {
                                _helper.DisplayError("Invalid expense ID. The data entered is Null or Empty.");
                            }

                            PauseAndClear();
                            break;

                        case 4:
                            var expenseIdInput = ExpenseView.ReadId();
                            if (!string.IsNullOrEmpty(expenseIdInput))
                            {
                                flag = ExpenseView.DeleteExpense(expenseIdInput);
                                if (flag)
                                {
                                    _helper.DisplaySuccess("Expense deleted.");
                                }
                                else
                                {
                                    _helper.DisplayError($"Try again...");
                                }
                            }
                            else
                            {
                                _helper.DisplayError("Invalid expense ID.");
                            }

                            PauseAndClear();
                            break;
                        case 5:
                            back = true;
                            PauseAndClear();
                            break;
                        default:
                            _helper.DisplayError("Invalid choice.");
                            PauseAndClear();
                            break;
                    }
                }
            }

            static void RunIncomeMenu()
            {
                IncomeView.Initialize(Service);
                bool back = false;
                while (!back)
                {
                    FinanceView.DisplayIncomeMenu();
                    switch (FinanceView.ReadChoice())
                    {
                        case 1:
                            bool flag = IncomeView.ReadIncomeDetails(true, TransactionType.Income);
                            if (flag)
                            {
                                _helper.DisplaySuccess("Income added.");
                            }
                            else
                            {
                                _helper.DisplayError("Try again...");
                            }

                            PauseAndClear();
                            break;

                        case 2:
                            IncomeView.DisplayAllIncomes();
                            PauseAndClear();
                            break;

                        case 3:
                            var id = IncomeView.ReadId();
                            if (!string.IsNullOrEmpty(id))
                            {
                                flag = IncomeView.ReadIncomeDetails(false, TransactionType.Income, id);
                                if (flag)
                                {
                                    _helper.DisplaySuccess("Income Updated.");
                                }
                                else
                                {
                                    _helper.DisplayError("Try again...");
                                }
                            }
                            else
                            {
                                _helper.DisplayError("Invalid Income ID. The data entered is Null or Empty.");
                            }

                            PauseAndClear();
                            break;
                        case 4:
                            var incomeIdInput = IncomeView.ReadId();
                            if (!string.IsNullOrEmpty(incomeIdInput))
                            {
                                flag = IncomeView.DeleteIncome(incomeIdInput);
                                if (flag)
                                {
                                    _helper.DisplaySuccess("Income deleted.");
                                }
                                else
                                {
                                    _helper.DisplayError($"Try again...");
                                }
                            }
                            else
                            {
                                _helper.DisplayError("Invalid Income ID.");
                            }

                            PauseAndClear();
                            break;

                        case 5:
                            back = true;
                            PauseAndClear();
                            break;

                        default:
                            _helper.DisplayError("Invalid choice.");
                            PauseAndClear();
                            break;
                    }
                }
            }
        }

        private static void ShowSummary()
        {
            decimal totalIncome = Service.GetAll(TransactionType.Income).Sum(x => x.Amount);
            decimal totalExpense = Service.GetAll(TransactionType.Expense).Sum(x => x.Amount);
            FinanceView.DisplaySummary(totalIncome, totalExpense);
        }

        private static void PauseAndClear()
        {
            Console.WriteLine("Please enter a key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void ShowSummaryPerUser()
        {
            string? userId = FinanceView.ReadId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                _helper.DisplayError("The entered user Id is null or empty, please try again.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(userId))
            {
                if (!Service.HasBothIncomeAndExpense(userId))
                {
                    _helper.DisplayError("There is no data for expense or income entered by the user. ");
                    return;
                }
                else
                {
                    _helper.DisplaySuccess("User has both datas for expense as well as income. ");
                }
            }
            else
            {
                _helper.DisplayError("Wrong input. Try again...");
                return;
            }

            var (totalIncome, totalExpense, balance) = Service.GetUserSummary(userId);
            FinanceView.DisplayUserSummary(totalIncome, totalExpense, balance);
        }
    }
}
