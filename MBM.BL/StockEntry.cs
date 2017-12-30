using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace MBM.BL
{
    [Serializable]
    [DataContract]
    public class StockEntry : INotifyPropertyChanged
    {
        public StockEntry()
        {
            this.Exchange = "AAAA";
            this.Symbol = "AAA";
            this.Date = DateTime.Parse("1/1/1754");
            this.PriceHigh = new Price();
            this.PriceLow = new Price();
            this.PriceOpen = new Price();
            this.PriceClose = new Price();
            this.PriceCloseAdjusted = new Price();
            PriceHigh.PropertyChanged += Price_PropertyChanged;
            PriceLow.PropertyChanged += Price_PropertyChanged;
            PriceOpen.PropertyChanged += Price_PropertyChanged;
            PriceClose.PropertyChanged += Price_PropertyChanged;
            PriceCloseAdjusted.PropertyChanged += Price_PropertyChanged;
        }



        public StockEntry(SqlDataReader reader) : this()
        {
            this.ID = uint.Parse(reader["stock_id"].ToString());
            this.Exchange = reader["exchange"].ToString();
            this.Symbol = reader["stock_symbol"].ToString();
            this.Date = DateTime.Parse(reader["date"].ToString());
            this.Volume = uint.Parse(reader["stock_volume"].ToString());

            this.PriceHigh.Amount = decimal.Parse(reader["stock_price_high"].ToString());
            this.PriceLow.Amount = decimal.Parse(reader["stock_price_low"].ToString());
            this.PriceOpen.Amount = decimal.Parse(reader["stock_price_open"].ToString());
            this.PriceClose.Amount = decimal.Parse(reader["stock_price_close"].ToString());
            this.PriceCloseAdjusted.Amount = decimal.Parse(reader["stock_price_adj_close"].ToString());
        }

        [DataMember]
        public uint ID { get; set; }

        private string _exchange;

        [DataMember]
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
                NotifyPropertyChanged("Exchange");
            }
        }

        private string _symbol;

        [DataMember]
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
                NotifyPropertyChanged("Symbol");
            }
        }

        private uint _volume;

        [DataMember]
        public uint Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
                NotifyPropertyChanged("Volume");
            }
        }

        private DateTime _date;

        [DataMember]
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged("Date");
            }
        }

        [DataMember]
        public Price PriceHigh { get; set; }

        [DataMember]
        public Price PriceLow { get; set; }

        [DataMember]
        public Price PriceOpen { get; set; }

        [DataMember]
        public Price PriceClose { get; set; }

        [DataMember]
        public Price PriceCloseAdjusted { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            string stockInformation;
            stockInformation = this.ID.ToString() + ", ";
            stockInformation += this.Exchange.ToString() + ", ";
            stockInformation += this.Symbol.ToString() + ", ";
            stockInformation += this.Date.ToString();

            return stockInformation;
        }

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Price_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }

    }
}
