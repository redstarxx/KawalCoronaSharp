using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KawalCoronaSharp.Enums
{
    /// <summary>
    /// Defines how to get the country name.
    /// </summary>
    public enum SearchMode
    {
        /// <summary>
        /// Gets the country which has the exact name of the desired country.
        /// </summary>
        Exact,

        /// <summary>
        /// Gets the country which has the closest matching name of the desired country.
        /// </summary>
        ClosestMatching
    }
}
