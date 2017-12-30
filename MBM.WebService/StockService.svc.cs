using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MBM.BL;
using MBM.DL;

namespace MBM.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IStockService
    {
        public string AddStockEntry(StockEntry stock)
        {
            string serverResponse;
            SQLStockRepository stockRepo = new SQLStockRepository();

            serverResponse = stockRepo.AddStockEntry(stock);

            return serverResponse;
        }

        public string DeleteStock(uint id)
        {
            string serverResponse;
            SQLStockRepository stockRepo = new SQLStockRepository();

            serverResponse = stockRepo.DeleteStock(id);

            return serverResponse;
        }

        public IEnumerable<StockEntry> GetStockEntries(Filter filter)
        {
            SQLStockRepository stockRepo = new SQLStockRepository();

            return stockRepo.GetStockEntries(filter);
        }

        public StockEntry GetStockEntry(uint id)
        {
            StockEntry serverResponse = new StockEntry();
            SQLStockRepository stockRepo = new SQLStockRepository();

            serverResponse = stockRepo.GetStockEntry(id);

            return serverResponse;
        }

        public string UpdateStockEntry(StockEntry stock)
        {
            string serverResponse;
            SQLStockRepository stockRepo = new SQLStockRepository();

            serverResponse = stockRepo.UpdateStockEntry(stock);

            return serverResponse;
        }
    }
}
