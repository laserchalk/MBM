using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.BL;
using MBM.DL.WCFStockService;

namespace MBM.DL
{
    /// <summary>Used for retrieving stock information from a web service</summary>
    public class WCFStockRepository : IStockRepository
    {
        /// <summary>Initialises an instance of WCFStockRepository</summary>
        public WCFStockRepository()
        {
            Client = new StockServiceClient();
        }



        /// <summary>Connection to web service</summary>
        private StockServiceClient Client;



        /// <summary>Adds a stock entry through a web service</summary>
        /// <exception cref="Exception">Thrown when failed to add stock entry though web service</exception>
        public string AddStockEntry(StockEntry stock)
        {
            try
            {
                string response;
                response = Client.AddStockEntry(stock);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add stock entry though web service", ex);
            }
        }

        /// <summary>Deletes a stock entry through a web service</summary>
        /// <exception cref="Exception">Thrown when failed to delete stock entry from a web service</exception>
        public string DeleteStock(uint id)
        {
            try
            {
                string response;
                response = Client.DeleteStock(id);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete stock entry through web service", ex);
            }
        }

        /// <summary>Gets a list of stock entries though a web service</summary>
        /// <exception cref="Exception">Thrown when failed to get stock entries through web service</exception>
        public IEnumerable<StockEntry> GetStockEntries(Filter filter)
        {
            try
            {
                List<StockEntry> response = new List<StockEntry>();
                response = Client.GetStockEntries(filter).ToList<StockEntry>();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get stock entries through web service", ex);
            }
        }

        /// <summary>Gets a stock entry through a web service</summary>
        /// <exception cref="Exception">Thrown when failed to get stock entry through web service</exception>
        public StockEntry GetStockEntry(uint id)
        {
            try
            {
                StockEntry response;
                response = Client.GetStockEntry(id);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get stock entry through web service", ex);
            }
        }

        /// <summary>Updates values for a stock entry through a web service</summary>
        /// <exception cref="Exception">Thrown when failed to update stock entry through web service</exception>
        public string UpdateStockEntry(StockEntry stock)
        {
            try
            {
                string response;
                response = Client.UpdateStockEntry(stock);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update stock entry through web service", ex);
            }
        }
    }
}
