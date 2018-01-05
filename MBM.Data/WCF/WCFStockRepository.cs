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
        public WCFStockRepository()
        {
            _stockClient = new StockServiceClient();
        }

        private StockServiceClient _stockClient;

        public string AddStockEntry(StockEntry stock)
        {
            string response;
            response = _stockClient.AddStockEntry(stock);

            return response;
        }

        public string DeleteStock(uint id)
        {
            string response;
            response = _stockClient.DeleteStock(id);

            return response;
        }

        public IEnumerable<StockEntry> GetStockEntries(Filter filter)
        {
            List<StockEntry> response = new List<StockEntry>();
            response = _stockClient.GetStockEntries(filter).ToList<StockEntry>();

            return response;
        }

        public StockEntry GetStockEntry(uint id)
        {
            StockEntry response;
            response = _stockClient.GetStockEntry(id);

            return response;
        }

        public string UpdateStockEntry(StockEntry stock)
        {
            string response;
            response = _stockClient.UpdateStockEntry(stock);

            return response;
        }
    }
}
