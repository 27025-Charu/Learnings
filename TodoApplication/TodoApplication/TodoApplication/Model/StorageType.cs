using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication.Model
{
    /// <summary>
    /// Specifies the type of storage used in the application.
    /// </summary>
    internal enum StorageType
    {
        /// <summary>
        /// Storage is in memory.
        /// </summary>
        InMemory = 1,

        /// <summary>
        /// Storage is in a CSV file.
        /// </summary>
        Json,

        /// <summary>
        /// Storage type is invalid or unspecified.
        /// </summary>
        Invalid,
    }
}
