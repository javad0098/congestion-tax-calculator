using CongestionTaxApp.Models;
using System.Collections.Generic;

namespace CongestionTaxApp.Data
{
    public class TollDataProvider : ITollDataProvider
    {
        private readonly HashSet<string> _tollFreeHolidays;
        private readonly List<TollFeeEntry> _tollFees;

        public TollDataProvider()
        {
            _tollFreeHolidays = new HashSet<string>
            {
                "2013-01-01",
                "2013-03-28",
                "2013-03-29",
                "2013-04-01",
                "2013-04-30",
                "2013-05-01",
                "2013-05-08",
                "2013-05-09",
                "2013-06-05",
                "2013-06-06",
                "2013-06-21",
                "2013-11-01",
                "2013-12-24",
                "2013-12-25",
                "2013-12-26",
                "2013-12-31"
            };

            _tollFees = new List<TollFeeEntry>
            {
                new TollFeeEntry((6, 0), (6, 29), 8),
                new TollFeeEntry((6, 30), (6, 59), 13),
                new TollFeeEntry((7, 0), (7, 59), 18),
                new TollFeeEntry((8, 0), (8, 29), 13),
                new TollFeeEntry((8, 30), (14, 59), 8),
                new TollFeeEntry((15, 0), (15, 29), 13),
                new TollFeeEntry((15, 30), (16, 59), 18),
                new TollFeeEntry((17, 0), (17, 59), 13),
                new TollFeeEntry((18, 0), (18, 29), 8),
                new TollFeeEntry((18, 30), (23, 59), 0)
            };
        }

        public HashSet<string> GetTollFreeHolidays() => _tollFreeHolidays;

        public List<TollFeeEntry> GetTollFees() => _tollFees;
    }
}
