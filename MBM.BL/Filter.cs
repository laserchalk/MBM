using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace MBM.BL
{
    /// <summary>
    /// Used for filtering stock data in IStockRepository.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Filter : ILoggable 
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Filter"/> class
        /// </summary>
        public Filter()
        {
            this.SelectedSymbol = "all symbols";
            this.OpenMin = new Price();
            this.OpenMax = new Price();
            this.CloseMin = new Price();
            this.CloseMax = new Price();
            this.CloseAdjustedMin = new Price();
            this.CloseAdjustedMax = new Price();
            this.HighMin = new Price();
            this.HighMax = new Price();
            this.LowMin = new Price();
            this.LowMax = new Price();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Filter"/> class using an SqlDataReader
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when trying to parse a null value</exception>
        /// <exception cref="FormatException">Thrown when trying to parse invalid values</exception>
        /// <exception cref="OverflowException">Thrown when trying to parse a value outside of the valid range</exception>
        /// <param name='reader'>should contain filter data</param>  
        public Filter(SqlDataReader reader) : this()
        {
            if (reader.Read())
            {
                this.DateStart = DateTime.Parse(reader["MinDate"].ToString());
                this.DateEnd = DateTime.Parse(reader["MaxDate"].ToString());
                this.VolumeMin = uint.Parse(reader["MinVolume"].ToString());
                this.VolumeMax = uint.Parse(reader["MaxVolume"].ToString());

                this.OpenMin.Amount = decimal.Parse(reader["MinPriceOpen"].ToString());
                this.OpenMax.Amount = decimal.Parse(reader["MaxPriceOpen"].ToString());
                this.CloseMin.Amount = decimal.Parse(reader["MinPriceClose"].ToString());
                this.CloseMax.Amount = decimal.Parse(reader["MaxPriceClose"].ToString());
                this.CloseAdjustedMin.Amount = decimal.Parse(reader["MinPriceCloseAdj"].ToString());
                this.CloseAdjustedMax.Amount = decimal.Parse(reader["MaxPriceCloseAdj"].ToString());
                this.HighMin.Amount = decimal.Parse(reader["MinPriceHigh"].ToString());
                this.HighMax.Amount = decimal.Parse(reader["MaxPriceHigh"].ToString());
                this.LowMin.Amount = decimal.Parse(reader["MinPriceLow"].ToString());
                this.LowMax.Amount = decimal.Parse(reader["MaxPriceLow"].ToString());
            }
        }

        [DataMember]
        public DateTime DateStart { get; set; }

        [DataMember]
        public DateTime DateEnd { get; set; }

        [DataMember]
        public string SelectedSymbol { get; set; }

        [DataMember]
        public List<string> Symbols { get; set; }

        [DataMember]
        public uint VolumeMin { get; set; }

        [DataMember]
        public uint VolumeMax { get; set; }

        [DataMember]
        public Price OpenMin { get; set; }

        [DataMember]
        public Price OpenMax { get; set; }

        [DataMember]
        public Price CloseMin { get; set; }

        [DataMember]
        public Price CloseMax { get; set; }

        [DataMember]
        public Price CloseAdjustedMin { get; set; }

        [DataMember]
        public Price CloseAdjustedMax { get; set; }

        [DataMember]
        public Price HighMin { get; set; }

        [DataMember]
        public Price HighMax { get; set; }

        [DataMember]
        public Price LowMin { get; set; }

        [DataMember]
        public Price LowMax { get; set; }

        /// <summary>
        /// Validates the filter
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when filter values are outside valid range</exception>
        public void Validate()
        {
            if (DateStart > DateEnd) throw new ArgumentException("Invalid date range. First date can't be greater than second date.");
            if (VolumeMin > VolumeMax) throw new ArgumentException("Invalid volume range. First volume can't be greater than second volume.");
            if (OpenMin.Amount > OpenMax.Amount) throw new ArgumentException("Invalid open price range. First price can't be greater than second price.");
            if (CloseMin.Amount > CloseMax.Amount) throw new ArgumentException("Invalid close price range. First price can't be greater than second price.");
            if (CloseAdjustedMin.Amount > CloseAdjustedMax.Amount) throw new ArgumentException("Invalid close adjusted price range. First price can't be greater than second price.");
            if (HighMin.Amount > HighMax.Amount) throw new ArgumentException("Invalid high price range. First price can't be greater than second price.");
            if (LowMin.Amount > LowMax.Amount) throw new ArgumentException("Invalid low price range. First price can't be greater than second price.");
        }

        /// <summary>
        /// Returns the filter as a string
        /// </summary>
        /// <exception cref="FormatException">Thrown when date values have an invalid format</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when date values are outside of valid range</exception>
        public override string ToString()
        {
            string filterInformation;

            filterInformation = this.DateStart.ToString("dd/MM/yyyy") + ",";
            filterInformation += this.DateEnd.ToString("dd/MM/yyyy") + ",";
            filterInformation += this.SelectedSymbol.ToString() + ",";
            filterInformation += this.VolumeMin.ToString() + ",";
            filterInformation += this.VolumeMax.ToString() + ",";
            filterInformation += this.OpenMin.ToString() + ",";
            filterInformation += this.OpenMax.ToString() + ",";
            filterInformation += this.CloseMin.ToString() + ",";
            filterInformation += this.CloseMax.ToString() + ",";
            filterInformation += this.CloseAdjustedMin.ToString() + ",";
            filterInformation += this.CloseAdjustedMax.ToString() + ",";
            filterInformation += this.HighMin.ToString() + ",";
            filterInformation += this.HighMax.ToString() + ",";
            filterInformation += this.LowMin.ToString() + ",";
            filterInformation += this.LowMax.ToString();

            return filterInformation;
        }

        /// <summary>
        /// Returns the filter as a string
        /// </summary>
        /// <exception cref="FormatException">Thrown when date values have an invalid format</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when date values are outside of valid range</exception>
        public string Log()
        {
            return ToString();
        }
    }
}
