using System;

namespace Raspador.Models
{
    public class Stock
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string LastPrice { get; set; }
        public string PriceChange { get; set; }
        public string PricePercentChange { get; set; }
        public string Shares { get; set; }
        public string CostBasis { get; set; }
        public string MarketValue { get; set; }
        public string DayGainChange { get; set; }
        public string DayGainPercentChange { get; set; }
        public string TotalGainChange { get; set; }
        public string TotalGainPercentChange { get; set; }
        public string NumOfLots { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
//        public int SnapshotId { get; set; }
        
        public virtual SnapshotInfo SnapshotInfo { get; set; }
    }
}