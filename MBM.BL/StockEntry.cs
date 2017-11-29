using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace MBM.BL
{
    [Serializable]
    public class StockEntry
    {
        public StockEntry()
        {
            Exchange = "AAAA";
            Symbol = "AAA";
            PriceHigh = new Price();
            PriceLow = new Price();
            PriceOpen = new Price();
            PriceClose = new Price();
            PriceCloseAdjusted = new Price();
        }

        public StockEntry(SqlDataReader reader)
        {
            this.ID = uint.Parse(reader["stock_id"].ToString());
            this.Exchange = reader["exchange"].ToString();
            this.Symbol = reader["stock_symbol"].ToString();
            this.Date = DateTime.Parse(reader["date"].ToString());
            this.Volume = uint.Parse(reader["stock_volume"].ToString());

            PriceHigh = new Price();
            PriceLow = new Price();
            PriceOpen = new Price();
            PriceClose = new Price();
            PriceCloseAdjusted = new Price();

            this.PriceHigh.Amount = decimal.Parse(reader["stock_price_high"].ToString());
            this.PriceLow.Amount = decimal.Parse(reader["stock_price_low"].ToString());
            this.PriceOpen.Amount = decimal.Parse(reader["stock_price_open"].ToString());
            this.PriceClose.Amount = decimal.Parse(reader["stock_price_close"].ToString());
            this.PriceCloseAdjusted.Amount = decimal.Parse(reader["stock_price_adj_close"].ToString());
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
                if(String.IsNullOrEmpty(value)) throw new ArgumentException("Exchange can't be null or empty");
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
                if (String.IsNullOrEmpty(value)) throw new ArgumentException("Symbol can't be null or empty");
                _symbol = value;
            }
        }
        public uint Volume { get; set; }
        public DateTime Date { get; set; }
        public Price PriceHigh { get; set; }
        public Price PriceLow { get; set; }
        public Price PriceOpen { get; set; }
        public Price PriceClose { get; set; }
        public Price PriceCloseAdjusted { get; set; }

    }
}
