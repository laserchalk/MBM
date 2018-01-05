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

            filter.HighStart.Amount = stockEntries.Min(y => y.PriceHigh.Amount);
            filter.HighEnd.Amount = stockEntries.Max(y => y.PriceHigh.Amount);
            filter.LowStart.Amount = stockEntries.Min(y => y.PriceLow.Amount);
            filter.LowEnd.Amount = stockEntries.Max(y => y.PriceLow.Amount);
            filter.OpenStart.Amount = stockEntries.Min(y => y.PriceOpen.Amount);
            filter.OpenEnd.Amount = stockEntries.Max(y => y.PriceOpen.Amount);
            filter.CloseStart.Amount = stockEntries.Min(y => y.PriceClose.Amount);
            filter.CloseEnd.Amount = stockEntries.Max(y => y.PriceClose.Amount);
            filter.CloseAdjustedStart.Amount = stockEntries.Min(y => y.PriceCloseAdjusted.Amount);
            filter.CloseAdjustedEnd.Amount = stockEntries.Max(y => y.PriceCloseAdjusted.Amount);

            filter.DateStart = stockEntries.Min(y => y.Date);
            filter.DateEnd = stockEntries.Max(y => y.Date);
            filter.VolumeStart = stockEntries.Min(y => y.Volume);
            filter.VolumeEnd = stockEntries.Max(y => y.Volume);

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
