using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents an amount for a specific income category.
    /// </summary>
    public class IncomeCategoryAmount
    {
        /// <summary>
        /// Gets or sets the income category.
        /// </summary>
        /// <value>
        /// The income category.
        /// </value>
        public IncomeCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the amount for the specified income category.
        /// </summary>
        /// <value> The amount for income category.</value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date associated with the income category amount.
        /// </summary>
        /// <value>
        /// The date for which the total amount is calculated.
        /// </value>
        public DateOnly Date { get; set; }
    }
}
