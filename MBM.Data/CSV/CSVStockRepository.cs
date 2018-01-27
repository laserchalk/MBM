using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.BL;
using System.IO;
using Common;

namespace MBM.DL
{
    /// <summary>Used for retrieving stock information from a csv file</summary>
    public class CSVStockRepository : IStockRepository
    {
        /// <summary>Initialises a new instance of CSVStockRepository with a default file path</summary>
        public CSVStockRepository()
        {
            FilePath = "MBMStockEntries.txt";
        }

        /// <summary>Initialises a new instance of CSVStockRepository with a custom file path</summary>
        public CSVStockRepository(string path)
        {
            FilePath = path;
        }



        /// <summary>The location the stock entries will be retrieve from</summary>
        private string FilePath { get; set; }



        /// <summary>Adds multiple stock entries to a csv file</summary>
        /// <exception cref="Exception">Thrown when failed to add a stock entries to a file</exception>
        public string AddStockEntries(IEnumerable<StockEntry> stockEntries)
        {
            try
            {
                FileManager.Save(stockEntries, FilePath);

                return "Stock entries saved";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save stock entries to file", ex);
            }
            
        }

        /// <summary>Adds a stock entry to a csv file</summary>
        /// <exception cref="Exception">Thrown when failed to add a stock entry to a file</exception>
        public string AddStockEntry(StockEntry stock)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FilePath, true))
                {
                    sw.WriteLine(stock);
                }

                return "Stock entry saved";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save stock entry to file", ex);
            }
        }

        /// <summary>Deletes a stock entry from a csv file</summary>
        /// <exception cref="NotImplementedException">Method not implemented</exception>
        public string DeleteStock(uint id)
        {
            throw new NotImplementedException();
        }

        /// <summary>Gets all stock entries from a csv file</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve filter values from a file</exception>
        public IEnumerable<StockEntry> GetStockEntries()
        {
            try
            {
                List<StockEntry> stockEntries = new List<StockEntry>();
                List<string> file = new List<string>();


                file = FileManager.Load(FilePath) as List<string>;

                foreach (string line in file)
                {
                    StockEntry stock = new StockEntry(line);
                    stockEntries.Add(stock);
                }


                return stockEntries;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get stock entries from file", ex);
            }
        }

        /// <summary>Gets a list of stock entries from a csv file and filters out the results</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve filter values from a file</exception>
        public IEnumerable<StockEntry> GetStockEntries(Filter filter)
        {
            try
            {
                List<StockEntry> stockEntries = new List<StockEntry>();
                IEnumerable<StockEntry> query;

                stockEntries = GetStockEntries() as List<StockEntry>;

                if (filter.SelectedSymbol == "all symbols")
                {
                    query = (from stock in stockEntries
                             where stock.Date >= filter.DateStart && stock.Date <= filter.DateEnd
                             && stock.Volume >= filter.VolumeMin && stock.Volume <= filter.VolumeMax
                             && stock.PriceOpen.Amount >= filter.OpenMin.Amount && stock.PriceOpen.Amount <= filter.OpenMax.Amount
                             && stock.PriceClose.Amount >= filter.CloseMin.Amount && stock.PriceClose.Amount <= filter.CloseMax.Amount
                             && stock.PriceCloseAdjusted.Amount >= filter.CloseAdjustedMin.Amount && stock.PriceCloseAdjusted.Amount <= filter.CloseAdjustedMax.Amount
                             && stock.PriceHigh.Amount >= filter.HighMin.Amount && stock.PriceHigh.Amount <= filter.HighMax.Amount
                             && stock.PriceLow.Amount >= filter.LowMin.Amount && stock.PriceLow.Amount <= filter.LowMax.Amount
                             select stock)
                             as IEnumerable<StockEntry>;
                }
                else
                {
                    query = (from stock in stockEntries
                             where stock.Date >= filter.DateStart && stock.Date <= filter.DateEnd
                             && stock.Volume >= filter.VolumeMin && stock.Volume <= filter.VolumeMax
                             && stock.PriceOpen.Amount >= filter.OpenMin.Amount && stock.PriceOpen.Amount <= filter.OpenMax.Amount
                             && stock.PriceClose.Amount >= filter.CloseMin.Amount && stock.PriceClose.Amount <= filter.CloseMax.Amount
                             && stock.PriceCloseAdjusted.Amount >= filter.CloseAdjustedMin.Amount && stock.PriceCloseAdjusted.Amount <= filter.CloseAdjustedMax.Amount
                             && stock.PriceHigh.Amount >= filter.HighMin.Amount && stock.PriceHigh.Amount <= filter.HighMax.Amount
                             && stock.PriceLow.Amount >= filter.LowMin.Amount && stock.PriceLow.Amount <= filter.LowMax.Amount
                             && stock.Symbol == filter.SelectedSymbol
                             select stock)
                             as IEnumerable<StockEntry>;
                }

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get stock entries from file", ex);
            }
        }

        /// <summary>Gets a stock entry from a csv file</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve filter values from a file</exception>
        public StockEntry GetStockEntry(uint id)
        {
            try
            {
                List<StockEntry> stockEntries = new List<StockEntry>();

                stockEntries = GetStockEntries() as List<StockEntry>;
                var query = (from entry in stockEntries
                             where entry.ID == id
                             select entry)
                            as List<StockEntry>;

                return query[0];
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get stock entry from file", ex);
            }
        }

        /// <summary>Updates values for a stock entry in a csv file</summary>
        /// <exception cref="NotImplementedException">Method not implemented</exception>
        public string UpdateStockEntry(StockEntry stock)
        {
            throw new NotImplementedException();
        }


    }
}
