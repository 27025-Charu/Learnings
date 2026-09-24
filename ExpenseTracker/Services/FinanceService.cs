using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Interface;
using ExpenseTracker.Model;
using ExpenseTracker.Repository;
using ExpenseTracker.View;

namespace ExpenseTracker.Services
{
    /// <summary>
    /// Provides services for managing expenses and incomes.
    /// </summary>
    public class FinanceService
    {
        private readonly IRepository _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FinanceService"/> class with the specified repository.
        /// </summary>
        /// <param name="repo">The repository to use for managing expenses and incomes.</param>
        public FinanceService(IRepository repo)
        {
            this._repo = repo;
        }

        /// <summary>
        /// Adds a transaction to the repository.
        /// </summary>
        /// <param name="transaction">The transaction to add.</param>
        /// <param name="type">The type of the transaction.</param>
        public void Add(ITransaction transaction, TransactionType type)
        {
            this._repo.Add(transaction);
        }

        /// <summary>
        /// Retrieves all transactions of the specified type from the repository.
        /// </summary>
        /// <param name="type">The type of transactions to retrieve (Income or Expense).</param>
        /// <returns>A list of transactions matching the specified type.</returns>
        public List<ITransaction> GetAll(TransactionType type)
        {
            return this._repo.GetByType(type);
        }

        /// <summary>
        /// Retrieves the transaction type for a transaction with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction.</param>
        /// <param name="type">The type of the transaction to retrieve.</param>
        /// <returns>
        /// The <see cref="ITransaction"/> of the transaction if found; otherwise, <c>null</c>.
        /// </returns>
        public ITransaction GetById(string id, TransactionType type)
        {
            var transaction = this._repo.GetById(id, type);
            if (transaction == null)
            {
                return null!;
            }

            return transaction;
        }

        /// <summary>
        /// Whether the id with that specific transaction type is present in the repo or not.
        /// </summary>
        /// <param name="id">The transaction id that needs to be searched. </param>
        /// <param name="type">In which transaction type this id is to be searched. </param>
        /// <returns>Returns whether the id is present or not present using true or false. </returns>
        public bool Exists(string id, TransactionType type)
        {
            return this._repo.GetAll(type).Any(item => item.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Updates an existing transaction in the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction to update.</param>
        /// <param name="transaction">The transaction object containing updated values.</param>
        /// <returns>
        /// <c>true</c> if the transaction was found and updated; otherwise, <c>false</c>.
        /// </returns>
        public bool Update(string id, ITransaction transaction)
        {
            var existingTransaction = this._repo.GetById(id, transaction.Type);
            if (existingTransaction == null)
            {
                Console.WriteLine($"Transaction-{transaction.Type} with id {id} not found.");
                return false;
            }

            transaction.Id = id;
            this._repo.Update(transaction);
            Console.WriteLine("Successfully updated the data. ");
            return true;
        }

        /// <summary>
        /// Deletes a transaction by its unique identifier and type.
        /// </summary>
        /// <param name="id">The unique identifier of the transaction to delete.</param>
        /// <param name="type">The type of the transaction to delete (Income or Expense).</param>
        /// <returns><c>true</c> if the transaction was successfully deleted; otherwise, <c>false</c>.</returns>
        public bool Delete(string id, TransactionType type)
        {
            return this._repo.Delete(id, type);
        }

        /// <summary>
        /// Gets the total income, total expense, and balance for a specific user.
        /// </summary>
        /// <param name="userId">The identifier of the user.</param>
        /// <returns>
        /// A tuple containing the total income, total expense, and the balance (total income minus total expense) for the user.
        /// </returns>
        public (decimal totalIncome, decimal totalExpense, decimal balance) GetUserSummary(string userId)
        {
            string incomeId = "INC" + userId;
            string expenseId = "EXP" + userId;
            decimal totalIncome = this._repo.GetByType(TransactionType.Income)
                .Where(x => x.Id.Equals(incomeId, StringComparison.OrdinalIgnoreCase))
                .Sum(x => x.Amount);
            decimal totalExpense = this._repo.GetByType(TransactionType.Expense)
                .Where(x => x.Id.Equals(expenseId, StringComparison.OrdinalIgnoreCase))
                .Sum(x => x.Amount);
            return (totalIncome, totalExpense, totalIncome - totalExpense);
        }

        /// <summary>
        /// Determines whether the specified user has at least one income and one expense.
        /// </summary>
        /// <param name="userId">The identifier of the user.</param>
        /// <returns>
        /// <c>true</c> if the user has both at least one income and one expense; otherwise, <c>false</c>.
        /// </returns>
        public bool HasBothIncomeAndExpense(string userId)
        {
            var hasIncome = this._repo.GetById("INC" + userId, TransactionType.Income);
            var hasExpense = this._repo.GetById("EXP" + userId, TransactionType.Expense);
            return hasIncome != null && hasExpense != null;
        }
    }
}
