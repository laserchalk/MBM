using MBM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL
{
    /// <summary>Used for retrieving filter information</summary>
    public interface IFilterRepository
    {
        /// <summary>Gets all values for the filter</summary>
        Filter GetFilter();

        /// <summary>Gets the min and max values for the filter</summary>
        Filter GetMinMaxValues();

        /// <summary>Gets a list of symbols for the filter</summary>
        IEnumerable<string> GetSymbols();
    }
}
