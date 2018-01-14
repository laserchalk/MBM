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
    public class CSVStockRepository : IStockRepository
    {
        public string AddStockEntries(IEnumerable<StockEntry> stockEntries)
        {
            try
            {
                FileManager.Save(stockEntries, "MBMStockEntries.txt");
                return "Stock entries saved";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string AddStockEntry(StockEntry stock)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("MBMStockEntries.txt", true))
                {
                    sw.WriteLine(stock);
                }

                return "Stock entry saved";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string DeleteStock(uint id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockEntry> GetStockEntries(Filter filter)
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

        public StockEntry GetStockEntry(uint id)
        {
            List<StockEntry> stockEntries = new List<StockEntry>();

            stockEntries = GetStockEntries() as List<StockEntry>;
            var query = (from entry in stockEntries
                        where entry.ID == id
                        select entry)
                        as List<StockEntry>;

            return query[0];
        }

        public string UpdateStockEntry(StockEntry stock)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockEntry> GetStockEntries()
        {
            List<StockEntry> stockEntries = new List<StockEntry>();
            List<string> file = new List<string>();


            file = FileManager.Load("MBMStockEntries.txt") as List<string>;

            foreach (string line in file)
            {
                StockEntry stock = new StockEntry(line);
                stockEntries.Add(stock);
            }


            return stockEntries;
        }
    }
}
