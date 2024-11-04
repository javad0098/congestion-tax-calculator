using CongestionTaxApp.Models;
using System.Collections.Generic;

namespace CongestionTaxApp.Data
{
    public interface ITollDataProvider
    {
        HashSet<string> GetTollFreeHolidays();
        List<TollFeeEntry> GetTollFees();
    }
}
