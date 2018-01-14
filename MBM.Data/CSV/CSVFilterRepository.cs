using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.BL;

namespace MBM.DL
{
    public class CSVFilterRepository : IFilterRepository
    {
        public Filter GetMinMaxValues()
        {
            Filter filter = new Filter();
            List<StockEntry> stockEntries = new List<StockEntry>();
            CSVStockRepository stockRepo = new CSVStockRepository();

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

        public IEnumerable<string> GetSymbols()
        {
            List<StockEntry> stockEntries = new List<StockEntry>();
            CSVStockRepository stockRepo = new CSVStockRepository();
            List<string> uniqueSymbols = new List<string>();

            stockEntries = stockRepo.GetStockEntries() as List<StockEntry>;

            uniqueSymbols = stockEntries
                            .Select(s => s.Symbol)
                            .Distinct().ToList();

            uniqueSymbols.Add("all symbols");

            return uniqueSymbols;
        }
    }
}
