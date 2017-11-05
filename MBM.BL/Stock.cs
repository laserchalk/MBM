using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL
{
    public class Stock
    {
        public Stock()
        {
            Prices = new Price[5];
        }

        public Stock(string exchange, string symbol, uint volume, DateTime date)
        {
            this.Exchange = exchange;
            this.Symbol = symbol;
            this.Volume = volume;
            this.Date = date;
        }

        public Stock(float priceOpen, float priceClose, float priceCloseAdjust, float priceHigh, float priceLow)
        {
            
        }

        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public uint Volume { get; set; }
        public DateTime Date { get; set; }
        public Price[] Prices { get; set; }



    }
}
