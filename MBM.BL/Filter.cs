using System;
using System.Collections.Generic;
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
            SelectedSymbol = "all symbols"
;       }

        public Filter(SqlDataReader reader)
        {
            if (reader.Read())
            {
                this.DateStart = DateTime.Parse(reader["MinDate"].ToString());
                this.DateEnd = DateTime.Parse(reader["MaxDate"].ToString());
                this.SelectedSymbol = "all symbols";
                this.VolumeStart = uint.Parse(reader["MinVolume"].ToString());
                this.VolumeEnd = uint.Parse(reader["MaxVolume"].ToString());
                this.OpenStart = decimal.Parse(reader["MinPriceOpen"].ToString());
                this.OpenEnd = decimal.Parse(reader["MaxPriceOpen"].ToString());
                this.CloseStart = decimal.Parse(reader["MinPriceClose"].ToString());
                this.CloseEnd = decimal.Parse(reader["MaxPriceClose"].ToString());
                this.CloseAdjustedStart = decimal.Parse(reader["MinPriceCloseAdj"].ToString());
                this.CloseAdjustedEnd= decimal.Parse(reader["MaxPriceCloseAdj"].ToString());
                this.HighStart = decimal.Parse(reader["MinPriceHigh"].ToString());
                this.HighEnd = decimal.Parse(reader["MaxPriceHigh"].ToString());
                this.LowStart = decimal.Parse(reader["MinPriceLow"].ToString());
                this.LowEnd = decimal.Parse(reader["MaxPriceLow"].ToString());
            }
        }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string SelectedSymbol { get; set; }
        public List<string> Symbols { get; set; }
        public uint VolumeStart { get; set; }
        public uint VolumeEnd { get; set; }
        public decimal OpenStart { get; set; }
        public decimal OpenEnd { get; set; }
        public decimal CloseStart { get; set; }
        public decimal CloseEnd { get; set; }
        public decimal CloseAdjustedStart { get; set; }
        public decimal CloseAdjustedEnd { get; set; }
        public decimal HighStart { get; set; }
        public decimal HighEnd { get; set; }
        public decimal LowStart { get; set; }
        public decimal LowEnd { get; set; }
    }
}
