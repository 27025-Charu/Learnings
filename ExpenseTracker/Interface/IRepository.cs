using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Model;

namespace ExpenseTracker.Interface
{
    /// <summary>
    /// Represents a contract for a repository that provides data access methods.
    /// </summary>
    /// <typeparam name="T">The type of entity managed by the repository. Must inherit from <see cref="CommonModel"/>.</typeparam>
    public interface IRepository
    {
        /// <summary>
        /// Adds a new transaction to the repository.
        /// </summary>
        /// <param name="transaction">The transaction to add.</param>
        void Add(ITransaction transaction);

        /// <summary>
        /// Deletes a transaction from the repository by its ID and type.
        /// </summary>
        /// <param name="incomeId">The ID of the transaction to delete.</param>
        /// <param name="type">The type of the transaction.</param>
        /// <returns>True when the delete is successful, else false. </returns>
        bool Delete(string incomeId, TransactionType type);

        /// <summary>
        /// Gets all transactions from the repository.
        /// </summary>
        /// <param name="type"> The type of transaction is to given. </param>
        /// <returns>A list of all transactions.</returns>
        List<ITransaction> GetAll(TransactionType type);

        /// <summary>
        /// Gets a transaction by its ID and type.
        /// </summary>
        /// <param name="id">The ID of the transaction.</param>
        /// <param name="type">The type of the transaction.</param>
        /// <returns>The transaction with the specified ID and type.</returns>
        ITransaction GetById(string id, TransactionType type);

        /// <summary>
        /// Updates an existing transaction in the repository.
        /// </summary>
        /// <param name="transaction">The transaction to update.</param>
        void Update(ITransaction transaction);

        /// <summary>
        /// Gets all transactions of a specific type.
        /// </summary>
        /// <param name="type">The type of transactions to retrieve.</param>
        /// <returns>A list of transactions of the specified type.</returns>
        List<ITransaction> GetByType(TransactionType type);
    }
}
