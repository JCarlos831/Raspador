using System;
using System.Collections.Generic;

namespace Raspador.Models
{
    public class SnapshotInfo
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string NetWorth { get; set; }
        public string DayGainChange { get; set; }
        public string DayGainPercent { get; set; }
        public string TotalGain { get; set; }
        public string TotalGainPercent { get; set; }
        public DateTime Date { get; set; }
        
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}