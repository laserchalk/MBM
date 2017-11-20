using MBM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL
{
    public interface IStockRepository
    {
        int AddStockEntry(StockEntry stock);
        StockEntry GetStockEntry(uint id);
        IEnumerable<StockEntry> GetStockEntries(Filter filter);
        void UpdateStockEntry(StockEntry stock);
        void UpdateStockEntries(IEnumerable<StockEntry> stockEntries);
        void DeleteStock(uint id);

    }
}
