using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Model;

namespace ExpenseTracker.Interface
{
    /// <summary>
    /// Represents a financial transaction with amount, date, type, and category.
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        /// <value> The unique identity of the user. </value>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user associated with the transaction.
        /// </summary>
        /// <value>The unique identifier of the user.</value>
        string UserId { get; set; }

        /// <summary>
        /// Gets the amount of the transaction.
        /// </summary>
        /// <value> Amount is the value that needs to be stored in the category. </value>
        decimal Amount { get; }

        /// <summary>
        /// Gets or sets the date of the transaction.
        /// </summary>
        /// <value> Date is the current date or the date when the transaction is performed. </value>
        DateOnly Date { get; set; }

        /// <summary>
        /// Gets  the type of the transaction (e.g., income or expense).
        /// </summary>
        /// <value> Type of the transaction whether it is income or expense. </value>
        TransactionType Type { get; }
    }
}
