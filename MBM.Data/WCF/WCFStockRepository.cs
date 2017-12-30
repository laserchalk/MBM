using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.BL;
using MBM.DL.WCFStockService;

namespace MBM.DL
{
    public class WCFStockRepository : IStockRepository
    {
        private StockServiceClient _stockClient;

        public WCFStockRepository()
        {
            _stockClient = new StockServiceClient();
        }

        public string AddStockEntry(StockEntry stock)
        {
            string response;
            response = _stockClient.AddStockEntry(stock);

            return response;
        }

        public string DeleteStock(uint id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockEntry> GetStockEntries(Filter filter)
        {
            List<StockEntry> response = new List<StockEntry>();
            response = _stockClient.GetStockEntries(filter).ToList<StockEntry>();

            return response;
        }

        public StockEntry GetStockEntry(uint id)
        {
            throw new NotImplementedException();
        }

        public string UpdateStockEntry(StockEntry stock)
        {
            throw new NotImplementedException();
        }
    }
}
