using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace MBM.BL
{

    /// <summary>
    /// Manages stock data.
    /// </summary>
    [Serializable]
    [DataContract]
    public class StockEntry : INotifyPropertyChanged , ILoggable
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


        /// <summary>
        /// Initialises a new instance of the <see cref="StockEntry"/> class using an SqlDataReader
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when trying to parse a null value</exception>
        /// <exception cref="FormatException">Thrown when trying to parse invalid values</exception>
        /// <exception cref="OverflowException">Thrown when trying to parse a value outside of the valid range</exception>
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

        /// <summary>
        /// Initialises a new instance of the <see cref="StockEntry"/> class using a string with comma separated values
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when trying to parse a null value</exception>
        /// <exception cref="FormatException">Thrown when trying to parse invalid values</exception>
        /// <exception cref="OverflowException">Thrown when trying to parse a value outside of the valid range</exception>
        public StockEntry(string line) : this()
        {
            List<string> values = new List<string>();

            values = new List<string>(line.Split(','));

            this.ID = uint.Parse(values[0]);
            this.Exchange = values[1];
            this.Symbol = values[2];
            this.Volume = uint.Parse(values[3]);
            this.Date = DateTime.Parse(values[4]);

            this.PriceHigh.Amount = decimal.Parse(values[5]);
            this.PriceLow.Amount = decimal.Parse(values[6]);
            this.PriceOpen.Amount = decimal.Parse(values[7]);
            this.PriceClose.Amount = decimal.Parse(values[8]);
            this.PriceCloseAdjusted.Amount = decimal.Parse(values[9]);
        }

        [DataMember]
        public uint ID { get; set; }

        private string _exchange;

        /// <summary>
        /// Gets or sets Exchange
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if setting a value with more than 5 character or using a value that is null or empty</exception>
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

        /// <summary>
        /// Gets or sets Exchange
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if setting a value with more than 4 character or using a value that is null or empty</exception>
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

        /// <summary>
        /// Occurs when a property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Returns the StockEntry as a string
        /// </summary>
        /// <exception cref="FormatException">Thrown when date values have an invalid format</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when date values are outside of valid range</exception>
        public override string ToString()
        {
            string stockInformation;

            stockInformation = this.ID.ToString() + ",";
            stockInformation += this.Exchange.ToString() + ",";
            stockInformation += this.Symbol.ToString() + ",";
            stockInformation += this.Volume.ToString() + ",";
            stockInformation += this.Date.ToString("dd/MM/yyyy") + ",";
            stockInformation += this.PriceHigh.ToString() + ",";
            stockInformation += this.PriceLow.ToString() + ",";
            stockInformation += this.PriceOpen.ToString() + ",";
            stockInformation += this.PriceClose.ToString() + ",";
            stockInformation += this.PriceCloseAdjusted.ToString();

            return stockInformation;
        }

        /// <summary>
        /// Returns the StockEntry as a string
        /// </summary>
        /// <exception cref="FormatException">Thrown when date values have an invalid format</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when date values are outside of valid range</exception>
        public string Log()
        {
            return ToString();
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
