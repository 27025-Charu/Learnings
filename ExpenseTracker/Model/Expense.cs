using System;
using ExpenseTracker.Interface;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents an expense entry with an identifier, amount, date, and category.
    /// </summary>
    public class Expense : ITransaction
    {
        /// <summary>
        /// Gets or sets the unique identifier for the expense entry.
        /// </summary>
        /// <value>
        /// The unique identifier for the expense entry.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier associated with the expense entry.
        /// </summary>
        /// <value>
        /// The user identifier associated with the expense entry.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the list of expense category amounts for this expense entry.
        /// </summary>
        /// <value>
        /// The list of expense category amounts.
        /// </value>
        public List<ExpenseCategoryAmount> Entries { get; set; } = new List<ExpenseCategoryAmount>();

        /// <summary>
        /// Gets the transaction type for this expense entry.
        /// </summary>
        /// <value>
        /// The transaction type for this expense entry.
        /// </value>
        public TransactionType Type
        {
            get => TransactionType.Expense;
        }

        /// <summary>
        /// Gets or sets the date associated with the expense entry.
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
        /// Gets the total amount for this expense entry by summing all category amounts.
        /// </summary>
        /// <value> The total amount for this expense.</value>
        public decimal Amount
        {
            get => this.Entries.Sum(x => x.Amount);
        }
    }
}
