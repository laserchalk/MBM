using MBM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL
{
    /// <summary>Interface for retrieving stock information</summary>
    public interface IStockRepository
    {
        /// <summary>Adds a stock entry to a data source</summary>
        string AddStockEntry(StockEntry stock);

        /// <summary>Gets a stock entry from a data source</summary>
        StockEntry GetStockEntry(uint id);

        /// <summary>Gets a list of stock entries from a data source</summary>
        IEnumerable<StockEntry> GetStockEntries(Filter filter);

        /// <summary>Updates values for a stock entry in a data source</summary>
        string UpdateStockEntry(StockEntry stock);

        /// <summary>Deletes a stock entry in a data source</summary>
        string DeleteStock(uint id);

    }
}
