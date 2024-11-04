using CongestionTaxApp.Common;
using CongestionTaxApp.Data;
using CongestionTaxApp.Helper;
using CongestionTaxApp.Interfaces;


namespace CongestionTaxApp.Features.CongestionTaxCalculator
{
    public class CongestionTaxCalculatorService : ICongestionTaxCalculatorService
    {
        private const int MaxDailyFee = 60;
        private readonly ILoggingService _logger;
        private readonly ITollDataProvider _tollDataProvider;

        public CongestionTaxCalculatorService(ITollDataProvider tollDataProvider, ILoggingService logger)
        {
            _tollDataProvider = tollDataProvider;
            _logger = logger;
        }

        public Task<int> GetTaxAsync(IVehicle vehicle, DateTime[] dates)
        {
            return Task.Run(() =>
            {
                if (dates == null || dates.Length == 0 || VehicleHelper.IsTollFreeVehicle(vehicle))
                {
                    _logger.LogWarning("No dates provided or vehicle is toll-free. Returning 0.");
                    return 0;
                }

                return dates
                    .OrderBy(d => d)
                    .GroupBy(d => d.Date)
                    .Select(dayGroup => CalculateDailyTax(dayGroup.ToList(), vehicle))
                    .Sum();
            });
        }

        private int CalculateDailyTax(List<DateTime> dayDates, IVehicle vehicle)
        {
            int dailyFee = 0;
            int intervalMaxFee = 0;
            DateTime intervalStart = dayDates.First();

            foreach (var date in dayDates)
            {
                int currentFee = GetTollFeeDay(date, vehicle);
                TimeSpan diff = date - intervalStart;

                if (diff.TotalMinutes <= 60)
                {
                    intervalMaxFee = Math.Max(intervalMaxFee, currentFee);
                }
                else
                {
                    dailyFee += intervalMaxFee;
                    intervalStart = date;
                    intervalMaxFee = currentFee;
                }

                if (dailyFee >= MaxDailyFee)
                {
                    _logger.LogInformation("Daily fee cap reached for date: {Date}. Returning max fee.", date);
                    return MaxDailyFee;
                }
            }

            dailyFee += intervalMaxFee;
            _logger.LogInformation("Total daily fee calculated: {DailyFee}", dailyFee);
            return Math.Min(dailyFee, MaxDailyFee);
        }

        private int GetTollFeeDay(DateTime date, IVehicle vehicle)
        {
            if (IsTollFreeDate(date) || VehicleHelper.IsTollFreeVehicle(vehicle))
            {
                _logger.LogInformation("No toll for date: {Date} (Toll-free)", date);
                return 0;
            }

            return GetTollFeeForTime(date.Hour, date.Minute);
        }

        private int GetTollFeeForTime(int hour, int minute)
        {
            var tollFees = _tollDataProvider.GetTollFees();

            foreach (var tollFee in tollFees)
            {
                if (IsWithinTimeRange(hour, minute, tollFee.Start, tollFee.End))
                {
                    _logger.LogInformation("Toll fee found: {Fee} SEK for time: {Hour}:{Minute}", tollFee.Fee, hour, minute);
                    return tollFee.Fee;
                }
            }

            _logger.LogWarning("No toll fee found for time: {Hour}:{Minute}. Returning 0.", hour, minute);
            return 0;
        }

        private static bool IsWithinTimeRange(int hour, int minute, (int StartHour, int StartMinute) start, (int EndHour, int EndMinute) end)
        {
            return (hour > start.StartHour || (hour == start.StartHour && minute >= start.StartMinute)) &&
                   (hour < end.EndHour || (hour == end.EndHour && minute <= end.EndMinute));
        }

        private bool IsTollFreeDate(DateTime date)
        {
            if (date.Month == 7 || date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                return true;

            var tollFreeHolidays = _tollDataProvider.GetTollFreeHolidays();
            return tollFreeHolidays.Contains(date.ToString("yyyy-MM-dd"));
        }
    }
}
