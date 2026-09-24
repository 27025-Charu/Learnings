using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents the available menu options for expense management.
    /// </summary>
    internal enum ExpenseMenu
    {
        /// <summary>
        /// Option to add a new expense entry.
        /// </summary>
        AddExpense = 1,

        /// <summary>
        /// Option to view all expense entries.
        /// </summary>
        ViewExpenses = 2,

        /// <summary>
        /// Option to edit an existing expense entry.
        /// </summary>
        EditExpense = 3,

        /// <summary>
        /// Option to delete an existing expense entry.
        /// </summary>
        DeleteExpense = 4,

        /// <summary>
        /// Option to exit the application.
        /// </summary>
        Exit = 5,
    }
}
