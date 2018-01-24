using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace MBM.BL
{
    ///<summary>Used for holding max and min filter data.</summary>
    [Serializable]
    [DataContract]
    public class Filter : EntityBase 
    {
        /// <summary>Initialises a new instance of <see cref="Filter"/> </summary>
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

        /// <summary>Initialises a new instance of <see cref="Filter"/> using an SqlDataReader</summary>
        /// <exception cref="ArgumentException">Thrown when failed to initialising Filter with SqlDataReader</exception>
        public Filter(SqlDataReader reader) : this()
        {
            try
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
            catch (Exception)
            {
                throw new ArgumentException("Failed to initialise Filter with SqlDataReader parameter");
            }
        }

        /// <summary>Gets or sets DateStart</summary>
        [DataMember]
        public DateTime DateStart { get; set; }

        /// <summary>Gets or sets DateEnd</summary>
        [DataMember]
        public DateTime DateEnd { get; set; }

        /// <summary>Gets or sets SelectedSymbol</summary>
        [DataMember]
        public string SelectedSymbol { get; set; }

        /// <summary>Gets or sets Symbols</summary>
        [DataMember]
        public List<string> Symbols { get; set; }

        /// <summary>Gets or sets VolumeMin</summary>
        [DataMember]
        public uint VolumeMin { get; set; }

        /// <summary>Gets or sets VolumeMax</summary>
        [DataMember]
        public uint VolumeMax { get; set; }

        /// <summary>Gets or sets OpenMin</summary>
        [DataMember]
        public Price OpenMin { get; set; }

        /// <summary>Gets or sets OpenMax</summary>
        [DataMember]
        public Price OpenMax { get; set; }

        /// <summary>Gets or sets CloseMin</summary>
        [DataMember]
        public Price CloseMin { get; set; }

        /// <summary>Gets or sets CloseMax</summary>
        [DataMember]
        public Price CloseMax { get; set; }

        /// <summary>Gets or sets CloseAdjustedMin</summary>
        [DataMember]
        public Price CloseAdjustedMin { get; set; }

        /// <summary>Gets or sets CloseAdjustedMax</summary>
        [DataMember]
        public Price CloseAdjustedMax { get; set; }

        /// <summary>Gets or sets HighMin</summary>
        [DataMember]
        public Price HighMin { get; set; }

        /// <summary>Gets or sets HighMax</summary>
        [DataMember]
        public Price HighMax { get; set; }

        /// <summary>Gets or sets LowMin</summary>
        [DataMember]
        public Price LowMin { get; set; }

        /// <summary>Gets or sets LowMax</summary>
        [DataMember]
        public Price LowMax { get; set; }


        /// <summary>Validates the filter</summary>
        /// <exception cref="ArgumentException">Thrown when filter values are outside valid range</exception>
        public override bool Validate()
        {
                if (DateStart > DateEnd) throw new ArgumentException("Invalid date range. First date can't be greater than second date.");
                if (VolumeMin > VolumeMax) throw new ArgumentException("Invalid volume range. First volume can't be greater than second volume.");
                if (OpenMin.Amount > OpenMax.Amount) throw new ArgumentException("Invalid open price range. First price can't be greater than second price.");
                if (CloseMin.Amount > CloseMax.Amount) throw new ArgumentException("Invalid close price range. First price can't be greater than second price.");
                if (CloseAdjustedMin.Amount > CloseAdjustedMax.Amount) throw new ArgumentException("Invalid close adjusted price range. First price can't be greater than second price.");
                if (HighMin.Amount > HighMax.Amount) throw new ArgumentException("Invalid high price range. First price can't be greater than second price.");
                if (LowMin.Amount > LowMax.Amount) throw new ArgumentException("Invalid low price range. First price can't be greater than second price.");

                return true;
        }

        /// <summary>Returns the filter as a string with comma seperated values</summary>
        /// <exception cref="ArgumentException">Thrown when date values have an invalid format or are out of range</exception>
        public override string ToString()
        {
            try
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
            catch (Exception)
            {
                throw new ArgumentException("Filter ToString() failed.");
            }
        }
    }
}
