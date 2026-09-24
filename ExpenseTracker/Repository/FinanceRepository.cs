using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Interface;
using ExpenseTracker.Model;
using ExpenseTracker.Services;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Provides methods for managing expenses and incomes in the finance tracker.
    /// </summary>
    internal class FinanceRepository : IRepository
    {
        private readonly List<ITransaction> _transactions = new ();

        /// <inheritdoc/>
        bool IRepository.Delete(string incomeId, TransactionType type)
        {
            var existing = ((IRepository)this).GetById(incomeId, type);
            if (existing != null)
            {
                this._transactions.Remove((ITransaction)existing);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        ITransaction IRepository.GetById(string id, TransactionType type)
        {
            try
            {
                return this._transactions.FirstOrDefault(x => x.Id == id && x.Type == type);
            }
            catch
            {
                Console.WriteLine("Transaction not found.");
                return null;
            }
        }

        /// <inheritdoc/>
        void IRepository.Add(ITransaction transaction)
        {
            this._transactions.Add(transaction);
        }

        /// <inheritdoc/>
        List<ITransaction> IRepository.GetAll(TransactionType type)
        {
            return this._transactions;
        }

        /// <inheritdoc/>
        void IRepository.Update(ITransaction transaction)
        {
            var existingData = ((IRepository)this).GetById(transaction.Id, transaction.Type);
            if (existingData != null)
            {
                this._transactions.Remove((ITransaction)existingData);
                this._transactions.Add(transaction);
            }
        }

        /// <inheritdoc/>
        List<ITransaction> IRepository.GetByType(TransactionType type)
        {
            return this._transactions.Where(x => x.Type == type).ToList();
        }

        /// <summary>
        /// Determines whether a transaction with the specified identifier and type exists.
        /// </summary>
        /// <param name="id">The identifier of the transaction.</param>
        /// <param name="type">The type of the transaction.</param>
        /// <returns><c>true</c> if the transaction exists; otherwise, <c>false</c>.</returns>
        public bool Exists(string id, TransactionType type)
        {
            return ((IRepository)this).GetById(id, type) != null;
        }

        // private static List<Income> CloneIncome(List<Income> list)
        // {
        //    if (list == null)
        //    {
        //        return new List<Income>();
        //    }

        // return list
        //        .Where(i => i != null)
        //        .Select(i => new Income
        //        {
        //            Id = i.Id,
        //            Amount = i.Amount,
        //            Date = i.Date,
        //            Category = i.Category,
        //        }).ToList();
        // }
    }
}
