using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL
{
    public class Filter
    {
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Symbol { get; set; }
        public uint VolumeStart { get; set; }
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
