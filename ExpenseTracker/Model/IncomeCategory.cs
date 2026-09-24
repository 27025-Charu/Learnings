using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Represents the category of an income entry.
    /// </summary>
    public enum IncomeCategory
    {
        /// <summary>
        /// Income from a regular salary.
        /// </summary>
        Salary = 1,

        /// <summary>
        /// Income from freelancing work.
        /// </summary>
        FreeLancing = 2,

        /// <summary>
        /// Income from other sources.
        /// </summary>
        Other = 3,
    }
}
