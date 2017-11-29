using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL
{
    public class Filter
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

        public Filter(SqlDataReader reader)
        {
            if (reader.Read())
            {
                this.DateStart = DateTime.Parse(reader["MinDate"].ToString());
                this.DateEnd = DateTime.Parse(reader["MaxDate"].ToString());
                this.SelectedSymbol = "all symbols";
                this.VolumeStart = uint.Parse(reader["MinVolume"].ToString());
                this.VolumeEnd = uint.Parse(reader["MaxVolume"].ToString());

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

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string SelectedSymbol { get; set; }
        public List<string> Symbols { get; set; }
        public uint VolumeStart { get; set; }
        public uint VolumeEnd { get; set; }

        public Price OpenStart { get; set; }
        public Price OpenEnd { get; set; }
        public Price CloseStart { get; set; }
        public Price CloseEnd { get; set; }
        public Price CloseAdjustedStart { get; set; }
        public Price CloseAdjustedEnd { get; set; }
        public Price HighStart { get; set; }
        public Price HighEnd { get; set; }
        public Price LowStart { get; set; }
        public Price LowEnd { get; set; }

        public void Validate()
        {
            if(DateStart > DateEnd) throw new ArgumentException("Invalid date range. First date can't be greater than second date.");
            if (VolumeStart > VolumeEnd) throw new ArgumentException("Invalid volume range. First volume can't be greater than second volume.");
            if (OpenStart.Amount > OpenEnd.Amount) throw new ArgumentException("Invalid open price range. First price can't be greater than second price.");
            if (CloseStart.Amount > CloseEnd.Amount) throw new ArgumentException("Invalid close price range. First price can't be greater than second price.");
            if (CloseAdjustedStart.Amount > CloseAdjustedEnd.Amount) throw new ArgumentException("Invalid close adjusted price range. First price can't be greater than second price.");
            if (HighStart.Amount > HighEnd.Amount) throw new ArgumentException("Invalid high price range. First price can't be greater than second price.");
            if (LowStart.Amount > LowEnd.Amount) throw new ArgumentException("Invalid low price range. First price can't be greater than second price.");
        }
    }
}
