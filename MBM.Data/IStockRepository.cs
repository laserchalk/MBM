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
        string AddStockEntry(StockEntry stock);
        StockEntry GetStockEntry(uint id);
        IEnumerable<StockEntry> GetStockEntries(Filter filter);
        string UpdateStockEntry(StockEntry stock);
        string DeleteStock(uint id);

    }
}
