using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    /// <summary>
    /// Specifies the available storage types for inventory data.
    /// </summary>
    public enum StorageType
    {
        /// <summary>
        /// Storage is in memory.
        /// </summary>
        InMemory = 1,

        /// <summary>
        /// Storage is in a CSV file.
        /// </summary>
        Csv = 2,

        /// <summary>
        /// Storage type is invalid or unspecified.
        /// </summary>
        Invalid = 0,
    }
}
