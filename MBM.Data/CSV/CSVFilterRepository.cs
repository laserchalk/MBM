using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.BL;

namespace MBM.DL
{
    /// <summary>Used for retrieving filter information from a csv file</summary>
    public class CSVFilterRepository : IFilterRepository
    {
        /// <summary>Initialises a new instance of CSVFilterRepository with a default file path</summary>
        public CSVFilterRepository()
        {
            FilePath = "MBMStockEntries.txt";
        }

        /// <summary>Initialises a new instance of CSVFilterRepository with a custom file path</summary>
        public CSVFilterRepository(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>Gets or sets the file path</summary>
        private string FilePath { get; set; }


        /// <summary>Gets filter values from the MBMStockEntries.txt file</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve filter values from a file</exception>
        public Filter GetFilter()
        {
            try
            {
                Filter filter = new Filter();

                filter = GetMinMaxValues();
                filter.Symbols = GetSymbols() as List<string>;

                return filter;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve filter values from local file", ex);
            }
        }



        /// <summary>Gets the min and max values from the MBMStockEntries.txt file</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve min and max values from a file</exception>
        public Filter GetMinMaxValues()
        {
            try
            {
                Filter filter = new Filter();
                List<StockEntry> stockEntries = new List<StockEntry>();
                CSVStockRepository stockRepo = new CSVStockRepository(FilePath);

                stockEntries = stockRepo.GetStockEntries() as List<StockEntry>;

                filter.HighMin.Amount = stockEntries.Min(y => y.PriceHigh.Amount);
                filter.HighMax.Amount = stockEntries.Max(y => y.PriceHigh.Amount);
                filter.LowMin.Amount = stockEntries.Min(y => y.PriceLow.Amount);
                filter.LowMax.Amount = stockEntries.Max(y => y.PriceLow.Amount);
                filter.OpenMin.Amount = stockEntries.Min(y => y.PriceOpen.Amount);
                filter.OpenMax.Amount = stockEntries.Max(y => y.PriceOpen.Amount);
                filter.CloseMin.Amount = stockEntries.Min(y => y.PriceClose.Amount);
                filter.CloseMax.Amount = stockEntries.Max(y => y.PriceClose.Amount);
                filter.CloseAdjustedMin.Amount = stockEntries.Min(y => y.PriceCloseAdjusted.Amount);
                filter.CloseAdjustedMax.Amount = stockEntries.Max(y => y.PriceCloseAdjusted.Amount);

                filter.DateStart = stockEntries.Min(y => y.Date);
                filter.DateEnd = stockEntries.Max(y => y.Date);
                filter.VolumeMin = stockEntries.Min(y => y.Volume);
                filter.VolumeMax = stockEntries.Max(y => y.Volume);

                return filter;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve min and max values from local file", ex);
            }
        }

        /// <summary>Gets the list of symbols from the MBMStockEntries.txt file</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve symbols from a file</exception>
        public IEnumerable<string> GetSymbols()
        {
            try
            {
                List<StockEntry> stockEntries = new List<StockEntry>();
                CSVStockRepository stockRepo = new CSVStockRepository(FilePath);
                List<string> uniqueSymbols = new List<string>();

                stockEntries = stockRepo.GetStockEntries() as List<StockEntry>;

                uniqueSymbols = stockEntries
                                .Select(s => s.Symbol)
                                .Distinct().ToList();

                uniqueSymbols.Add("all symbols");

                return uniqueSymbols;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve symbols from local file", ex);
            }
        }
    }
}
