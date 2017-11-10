using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL
{
    public class StockEntry
    {
        public StockEntry()
        {

        }

        public StockEntry(string exchange, string symbol, uint volume, DateTime date, decimal priceHigh, decimal priceLow, decimal priceOpen, decimal priceClose, decimal priceCloseAdjusted)
        {
            this.Exchange = exchange;
            this.Symbol = symbol;
            this.Volume = volume;
            this.Date = date;
            this.PriceHigh = priceHigh;
            this.PriceLow = priceLow;
            this.PriceOpen = priceOpen;
            this.PriceClose = priceClose;
            this.PriceCloseAdjusted = priceCloseAdjusted;
        }

        public uint ID { get; set; }

        private string _exchange;
        public string Exchange {
            get
            {
                return _exchange;
            }
            set
            {
                if(value.Length > 4) throw new ArgumentException("Exchange must have less than 5 characters");
                _exchange = value;
            }
        }

        private string _symbol;
        public string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                if (value.Length > 3) throw new ArgumentException("Symbol must have less than 4 characters");
                _symbol = value;
            }
        }
        public uint Volume { get; set; }
        public DateTime Date { get; set; }

        private decimal _priceHigh;
        public decimal PriceHigh
        {
            get
            {
                return _priceHigh;
            }
            set
            {
                if (value < 0) throw new ArgumentException("PriceHigh cannot be negative");
                _priceHigh = value;
            }
        }

        private decimal _priceLow;
        public decimal PriceLow
        {
            get
            {
                return _priceLow;
            }
            set
            {
                if (value < 0) throw new ArgumentException("PriceLow cannot be negative");
                _priceLow = value;
            }
        }

        private decimal _priceOpen;
        public decimal PriceOpen
        {
            get
            {
                return _priceOpen;
            }
            set
            {
                if (value < 0) throw new ArgumentException("PriceOpen cannot be negative");
                _priceOpen = value;
            }
        }

        private decimal _priceClose;
        public decimal PriceClose
        {
            get
            {
                return _priceClose;
            }
            set
            {
                if (value < 0) throw new ArgumentException("PriceClose cannot be negative");
                _priceClose = value;
            }
        }

        private decimal _priceCloseAdjusted;
        public decimal PriceCloseAdjusted
        {
            get
            {
                return _priceCloseAdjusted;
            }
            set
            {
                if (value < 0) throw new ArgumentException("PriceCloseAdjusted cannot be negative");
                _priceCloseAdjusted = value;
            }
        }


    }
}
