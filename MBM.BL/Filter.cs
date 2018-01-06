using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace MBM.BL
{
    [Serializable]
    [DataContract]
    public class Filter : ILoggable 
    {
        public Filter()
        {
            this.SelectedSymbol = "all symbols";
            this.OpenStart = new Price();
            this.OpenEnd = new Price();
            this.CloseStart = new Price();
            this.CloseEnd = new Price();
            this.CloseAdjustedStart = new Price();
            this.CloseAdjustedEnd = new Price();
            this.HighStart = new Price();
            this.HighEnd = new Price();
            this.LowStart = new Price();
            this.LowEnd = new Price();
        }

        public Filter(SqlDataReader reader) : this()
        {
            if (reader.Read())
            {
                this.DateStart = DateTime.Parse(reader["MinDate"].ToString());
                this.DateEnd = DateTime.Parse(reader["MaxDate"].ToString());
                this.VolumeStart = uint.Parse(reader["MinVolume"].ToString());
                this.VolumeEnd = uint.Parse(reader["MaxVolume"].ToString());

                this.OpenStart.Amount = decimal.Parse(reader["MinPriceOpen"].ToString());
                this.OpenEnd.Amount = decimal.Parse(reader["MaxPriceOpen"].ToString());
                this.CloseStart.Amount = decimal.Parse(reader["MinPriceClose"].ToString());
                this.CloseEnd.Amount = decimal.Parse(reader["MaxPriceClose"].ToString());
                this.CloseAdjustedStart.Amount = decimal.Parse(reader["MinPriceCloseAdj"].ToString());
                this.CloseAdjustedEnd.Amount = decimal.Parse(reader["MaxPriceCloseAdj"].ToString());
                this.HighStart.Amount = decimal.Parse(reader["MinPriceHigh"].ToString());
                this.HighEnd.Amount = decimal.Parse(reader["MaxPriceHigh"].ToString());
                this.LowStart.Amount = decimal.Parse(reader["MinPriceLow"].ToString());
                this.LowEnd.Amount = decimal.Parse(reader["MaxPriceLow"].ToString());
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
        public uint VolumeStart { get; set; }

        [DataMember]
        public uint VolumeEnd { get; set; }

        [DataMember]
        public Price OpenStart { get; set; }

        [DataMember]
        public Price OpenEnd { get; set; }

        [DataMember]
        public Price CloseStart { get; set; }

        [DataMember]
        public Price CloseEnd { get; set; }

        [DataMember]
        public Price CloseAdjustedStart { get; set; }

        [DataMember]
        public Price CloseAdjustedEnd { get; set; }

        [DataMember]
        public Price HighStart { get; set; }

        [DataMember]
        public Price HighEnd { get; set; }

        [DataMember]
        public Price LowStart { get; set; }

        [DataMember]
        public Price LowEnd { get; set; }

        public void Validate()
        {
            if (DateStart > DateEnd) throw new ArgumentException("Invalid date range. First date can't be greater than second date.");
            if (VolumeStart > VolumeEnd) throw new ArgumentException("Invalid volume range. First volume can't be greater than second volume.");
            if (OpenStart.Amount > OpenEnd.Amount) throw new ArgumentException("Invalid open price range. First price can't be greater than second price.");
            if (CloseStart.Amount > CloseEnd.Amount) throw new ArgumentException("Invalid close price range. First price can't be greater than second price.");
            if (CloseAdjustedStart.Amount > CloseAdjustedEnd.Amount) throw new ArgumentException("Invalid close adjusted price range. First price can't be greater than second price.");
            if (HighStart.Amount > HighEnd.Amount) throw new ArgumentException("Invalid high price range. First price can't be greater than second price.");
            if (LowStart.Amount > LowEnd.Amount) throw new ArgumentException("Invalid low price range. First price can't be greater than second price.");
        }

        public override string ToString()
        {
            string filterInformation;

            filterInformation = this.DateStart.ToString("dd/MM/yyyy") + ",";
            filterInformation += this.DateEnd.ToString("dd/MM/yyyy") + ",";
            filterInformation += this.SelectedSymbol.ToString() + ",";
            filterInformation += this.VolumeStart.ToString() + ",";
            filterInformation += this.VolumeEnd.ToString() + ",";
            filterInformation += this.OpenStart.ToString() + ",";
            filterInformation += this.OpenEnd.ToString() + ",";
            filterInformation += this.CloseStart.ToString() + ",";
            filterInformation += this.CloseEnd.ToString() + ",";
            filterInformation += this.CloseAdjustedStart.ToString() + ",";
            filterInformation += this.CloseAdjustedEnd.ToString() + ",";
            filterInformation += this.HighStart.ToString() + ",";
            filterInformation += this.HighEnd.ToString() + ",";
            filterInformation += this.LowStart.ToString() + ",";
            filterInformation += this.LowEnd.ToString();

            return filterInformation;
        }

        public string Log()
        {
            return ToString();
        }
    }
}
