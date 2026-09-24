using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents the available menu options in the expense tracker application.
    /// </summary>
    internal enum MainMenu
    {
        /// <summary>
        /// Option to manage incomes.
        /// </summary>
        IncomeMenu = 1,

        /// <summary>
        /// Option to manage expenses.
        /// </summary>
        ExpenseMenu = 2,

        /// <summary>
        /// Option to show a summary of incomes and expenses.
        /// </summary>
        ShowSummary = 3,

        /// <summary>
        /// Option to show a summary of incomes and expenses per user.
        /// </summary>
        ShowSummaryPerUser = 4,

        /// <summary>
        /// Option to exit the application.
        /// </summary>
        Exit = 5,
    }
}
