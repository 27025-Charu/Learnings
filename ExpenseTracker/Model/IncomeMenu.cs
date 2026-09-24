using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents the available menu options for managing income entries in the expense tracker application.
    /// </summary>
    internal enum IncomeMenu
    {
        /// <summary>
        /// Option to add a new income entry.
        /// </summary>
        AddIncome = 1,

        /// <summary>
        /// Option to view all income entries.
        /// </summary>
        ViewIncomes = 2,

        /// <summary>
        /// Option to edit an existing income entry.
        /// </summary>
        EditIncome = 3,

        /// <summary>
        /// Option to delete an existing income entry.
        /// </summary>
        DeleteIncome = 4,

        /// <summary>
        /// Option to exit the application.
        /// </summary>
        Exit = 5,
    }
}
