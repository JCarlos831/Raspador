using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raspador.Models
{
    public class SnapshotInfo
    {
        public string UserId { get; set; }
        [Key]
        public int SnapshotId { get; set; }
        public string NetWorth { get; set; }
        public string DayGainChange { get; set; }
        public string DayGainPercent { get; set; }
        public string TotalGain { get; set; }
        public string TotalGainPercent { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        
        public virtual List<Stock> Stocks { get; set; }
    }
}