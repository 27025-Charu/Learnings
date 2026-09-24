using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Specifies categories for an <see cref="Expense"/>.
    /// </summary>
    public enum ExpenseCategory
    {
        /// <summary>
        /// Expenses for food and groceries.
        /// </summary>
        Food = 1,

        /// <summary>
        /// Expenses for travel, transit, and transportation.
        /// </summary>
        Travel = 2,

        /// <summary>
        /// Household utilities, bills, and recurring services.
        /// </summary>
        Utilities = 3,

        /// <summary>
        /// Leisure, movies, events, and entertainment.
        /// </summary>
        Entertainment = 4,

        /// <summary>
        /// Any other uncategorized expense.
        /// </summary>
        Other = 5,
    }
}