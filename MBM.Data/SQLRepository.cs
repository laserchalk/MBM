using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.BL;

namespace MBM.Data
{
    public class SQLRepository : IStockRepository
    {
        public void AddStockEntry(StockEntry stock)
        {
            throw new NotImplementedException();
        }

        public void DeleteStock(uint id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockEntry> GetStockEntries(Filter filter)
        {
            throw new NotImplementedException();
        }

        public StockEntry GetStockEntry(uint id)
        {
            throw new NotImplementedException();
        }

        public void UpdateStockEntries(IEnumerable<StockEntry> stockEntries)
        {
            throw new NotImplementedException();
        }

        public void UpdateStockEntry(StockEntry stock)
        {
            throw new NotImplementedException();
        }
    }
}
