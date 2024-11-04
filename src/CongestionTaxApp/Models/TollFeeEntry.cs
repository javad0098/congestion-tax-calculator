namespace CongestionTaxApp.Models
{
    public class TollFeeEntry
    {
        public (int Hour, int Minute) Start { get; set; }
        public (int Hour, int Minute) End { get; set; }
        public int Fee { get; set; }

        public TollFeeEntry((int Hour, int Minute) start, (int Hour, int Minute) end, int fee)
        {
            Start = start;
            End = end;
            Fee = fee;
        }
    }
}
