using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Interface;
using ExpenseTracker.Model;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents an income entry with a category, amount, and date.
    /// </summary>
    public class Income : ITransaction
    {
        /// <summary>
        /// Gets or sets the unique identifier for the income entry.
        /// </summary>
        /// <value>
        /// The unique identifier for the income entry.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier associated with the income entry.
        /// </summary>
        /// <value>
        /// The user identifier associated with the income entry.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the date associated with the income entry.
        /// </summary>
        /// <value>
        /// The date of the first entry in the <see cref="Entries"/> list, or the current date if no entries exist.
        /// </value>
        public DateOnly Date
        {
            get => this.Entries.FirstOrDefault()?.Date ?? DateOnly.FromDateTime(DateTime.Now);
            set { }
        }

        /// <summary>
        /// Gets or sets the list of income category amounts for this income entry.
        /// </summary>
        /// <value>
        /// The list of income category amounts.
        /// </value>
        public List<IncomeCategoryAmount> Entries { get; set; } = new List<IncomeCategoryAmount>();

        /// <summary>
        /// Gets the transaction type for this income entry.
        /// </summary>
        /// <value>
        /// The transaction type for this income entry.
        /// </value>
        public TransactionType Type
        {
            get => TransactionType.Income;
        }

        /// <summary>
        /// Gets the total amount for this income entry by summing all category amounts.
        /// </summary>
        /// <value> The total amount for this income.</value>
        public decimal Amount
        {
            get => this.Entries.Sum(x => x.Amount);
        }
    }
}