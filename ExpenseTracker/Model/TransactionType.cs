using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Specifies the type of a transaction.
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Represents an income transaction.
        /// </summary>
        Income = 1,

        /// <summary>
        /// Represents an expense transaction.
        /// </summary>
        Expense = 2,
    }
}
