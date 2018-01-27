using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace MBM.BL
{

    /// <summary>Holds stock data.</summary>
    [Serializable]
    [DataContract]
    public class StockEntry : EntityBase, INotifyPropertyChanged
    {
        /// <summary>Initialises a new instance of <see cref="StockEntry"/></summary>
        /// <exception cref="Exception">Thrown when StockEntry failed to initialise</exception>
        public StockEntry()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Failed to initialise StockEntry", ex);
            }
        }


        /// <summary>Initialises a new instance of <see cref="StockEntry"/> using an SqlDataReader</summary>
        /// <exception cref="Exception">Thrown when StockEntry failed to initialise</exception>
        public StockEntry(SqlDataReader reader) : this()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Failed to initialise StockEntry with SqlDataReader parameter", ex);
            }
        }

        /// <summary>Initialises a new instance of  <see cref="StockEntry"/> using a string with comma separated values</summary>
        /// <exception cref="Exception">Thrown when StockEntry failed to initialise</exception>
        public StockEntry(string line) : this()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Failed to initialise StockEntry with string parameter", ex);
            }
        }

        /// <summary>Gets or sets ID</summary>
        [DataMember]
        public uint ID { get; set; }

        
        /// <summary>Gets or sets Exchange</summary>
        /// <exception cref="Exception">Thrown if setting a value with more than 5 character or using a value that is null or empty</exception>
        [DataMember]
        public string Exchange {
            get
            {
                return _exchange;
            }
            set
            {
                if(value.Length > 4) throw new Exception("Exchange must have less than 5 characters");
                if(String.IsNullOrEmpty(value)) throw new Exception("Exchange can't be null or empty");
                _exchange = value;
                NotifyPropertyChanged("Exchange");
            }
        }
        private string _exchange;

        
        /// <summary>Gets or sets Symbol</summary>
        /// <exception cref="Exception">Thrown if setting a value with more than 4 character or using a value that is null or empty</exception>
        [DataMember]
        public string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                if (value.Length > 3) throw new Exception("Symbol must have less than 4 characters");
                if (String.IsNullOrEmpty(value)) throw new Exception("Symbol can't be null or empty");
                _symbol = value;
                NotifyPropertyChanged("Symbol");
            }
        }
        private string _symbol;

        
        /// <summary>Gets or sets Volume</summary>
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
        private uint _volume;

        
        /// <summary>Gets or sets Date</summary>
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
        private DateTime _date;

        /// <summary>Gets or sets PriceHigh</summary>
        [DataMember]
        public Price PriceHigh { get; set; }

        /// <summary>Gets or sets PriceLow</summary>
        [DataMember]
        public Price PriceLow { get; set; }

        /// <summary>Gets or sets PriceOpen</summary>
        [DataMember]
        public Price PriceOpen { get; set; }

        /// <summary>Gets or sets PriceClose</summary>
        [DataMember]
        public Price PriceClose { get; set; }

        /// <summary>Gets or sets PriceCloseAdjusted</summary>
        [DataMember]
        public Price PriceCloseAdjusted { get; set; }


        /// <summary> Returns the StockEntry as a comma seperated string </summary>
        /// <exception cref="Exception">Thrown when date values have an invalid format or if values are outside of valid range.</exception>
        public override string ToString()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("StockEntry ToString() Failed.", ex);
            }
        }

        /// <summary> Invokes the property changed event for prices</summary>
        private void Price_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.NotifyPropertyChanged(e.PropertyName);
        }

        
    }
}
