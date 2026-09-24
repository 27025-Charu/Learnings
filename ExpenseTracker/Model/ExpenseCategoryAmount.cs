using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents the total amount spent for a specific expense category.
    /// </summary>
    public class ExpenseCategoryAmount
    {
        /// <summary>
        /// Gets or sets the expense category.
        /// </summary>
        /// <value>
        /// The category of the expense.
        /// </value>
        public ExpenseCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the category.
        /// </summary>
        /// <value>
        /// The total amount spent in the category.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date associated with the expense category amount.
        /// </summary>
        /// <value>
        /// The date for which the total amount is calculated.
        /// </value>
        public DateOnly Date { get; set; }
    }
}
