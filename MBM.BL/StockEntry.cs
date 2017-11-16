using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public StockEntry(SqlDataReader reader)
        {
                this.ID = uint.Parse(reader["stock_id"].ToString());
                this.Exchange = reader["exchange"].ToString();
                this.Symbol = reader["stock_symbol"].ToString();
                this.Date = DateTime.Parse(reader["date"].ToString());
                this.Volume = uint.Parse(reader["stock_volume"].ToString());
                this.PriceHigh = decimal.Parse(reader["stock_price_high"].ToString());
                this.PriceLow = decimal.Parse(reader["stock_price_low"].ToString());
                this.PriceOpen = decimal.Parse(reader["stock_price_open"].ToString());
                this.PriceClose = decimal.Parse(reader["stock_price_close"].ToString());
                this.PriceCloseAdjusted = decimal.Parse(reader["stock_price_adj_close"].ToString());
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
