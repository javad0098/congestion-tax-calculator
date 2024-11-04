using System;
using System.Linq;
using System.Threading.Tasks;
using CongestionTaxApp.Features.CongestionTaxCalculator;
using CongestionTaxApp.Common;
using CongestionTaxApp.Data;
using CongestionTaxApp.Interfaces;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CongestionTaxApp.Models;

namespace CongestionTaxApp.Tests
{

    // TODO:
    // Enhance this test class to improve clarity and maintainability:
    // - Consider mocking ITollDataProvider to decouple tests from specific data implementations.
    // - Implement parameterized tests for different vehicle types and dates to reduce redundancy and improve readability.
    // - Ensure tests cover a variety of scenarios, including edge cases related to toll-free vehicles and time intervals.

    public class CongestionTaxCalculatorTests
    {
        private readonly ICongestionTaxCalculatorService _calculatorService;
        private readonly IVehicle _vehicle;
        private readonly DateTime[] _dates;
        private readonly ITollDataProvider _tollDataProvider;

        public CongestionTaxCalculatorTests()
        {
            var serviceCollection = new ServiceCollection();
            // TODO: Consider mocking ITollDataProvider to improve test isolation and flexibility.
            // Directly reading from TollDataProvider here makes the test depend on specific implementation details, 
            // making it harder to test edge cases and less flexible in adapting to changes
            serviceCollection.AddSingleton<ITollDataProvider, TollDataProvider>();
            serviceCollection.AddSingleton<ILoggingService, LoggingService>();
            serviceCollection.AddTransient<ICongestionTaxCalculatorService, CongestionTaxCalculatorService>();
            serviceCollection.AddLogging(configure => configure.AddConsole());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _calculatorService = serviceProvider.GetRequiredService<ICongestionTaxCalculatorService>();
            _vehicle = new Car();
            // Directly retrieving ITollDataProvider from the service provider ties tests to the concrete implementation, making it harder to simulate different scenarios.
            _tollDataProvider = serviceProvider.GetRequiredService<ITollDataProvider>();

            _dates = new[]
            {
                DateTime.Parse("2013-01-14 21:00:00"),
                DateTime.Parse("2013-01-15 21:00:00"),
                DateTime.Parse("2013-02-07 06:23:27"),
                DateTime.Parse("2013-02-07 15:27:00"),
                DateTime.Parse("2013-02-08 06:27:00"),
                DateTime.Parse("2013-02-08 06:20:27"),
                DateTime.Parse("2013-02-08 14:35:00"),
                DateTime.Parse("2013-02-08 15:29:00"),
                DateTime.Parse("2013-02-08 15:47:00"),
                DateTime.Parse("2013-02-08 16:01:00"),
                DateTime.Parse("2013-02-08 16:48:00"),
                DateTime.Parse("2013-02-08 17:49:00"),
                DateTime.Parse("2013-02-08 18:29:00"),
                DateTime.Parse("2013-02-08 18:35:00"),
                DateTime.Parse("2013-03-26 14:25:00"),
                DateTime.Parse("2013-03-28 14:07:27")
            };
        }

        [Fact]
        public async Task ShouldBeZeroAfter1830()
        {
            var eveningDates = _dates.Where(d => d.TimeOfDay > new TimeSpan(18, 30, 0)).ToArray();
            var taxForEveningDates = await _calculatorService.GetTaxAsync(_vehicle, eveningDates);
            Assert.Equal(0, taxForEveningDates);
        }

        [Fact]
        public async Task ShouldBeZeroOnCertainTollFreeDates()
        {
            var tollFreeDates = _dates.Where(d =>
                _tollDataProvider.GetTollFreeHolidays().Contains(d.ToString("yyyy-MM-dd")) ||
                d.Month == 7).ToArray();

            var taxForTollFreeDates = await _calculatorService.GetTaxAsync(_vehicle, tollFreeDates);
            Assert.Equal(0, taxForTollFreeDates);
        }

        [Fact]
        public async Task CalculateShouldNotExceedMax60()
        {
            var february8Dates = _dates.Where(d => d.Date == new DateTime(2013, 2, 8)).ToArray();
            var taxForFeb08 = await _calculatorService.GetTaxAsync(_vehicle, february8Dates);
            Assert.Equal(60, taxForFeb08);
        }

        [Fact]
        public async Task CalculateTotalForAllDates()
        {
            int totalTax = await _calculatorService.GetTaxAsync(_vehicle, _dates);
            Console.WriteLine($"Total Tax: {totalTax}"); // Just displaying the total tax

            foreach (var date in _dates)
            {
                int taxForDate = await _calculatorService.GetTaxAsync(_vehicle, new[] { date });
                Console.WriteLine($"Date: {date}, Tax: {taxForDate}");
            }
            Assert.Equal(89, totalTax);
        }

        [Fact]
        public async Task TollFreeVehiclesShouldReturnZeroTax()
        {
            // Arrange: Create a list of toll-free vehicles
            var tollFreeVehicles = new IVehicle[]
            {
                new Motorbike(),
                new Bus(),
                new Emergency(),
                new Diplomat(),
                new Foreign(),
                new Military()
            };

            // Arrange: Prepare a date array for testing
            var dates = _dates;

            foreach (var vehicle in tollFreeVehicles)
            {
                var taxForVehicle = await _calculatorService.GetTaxAsync(vehicle, dates);
                Assert.Equal(0, taxForVehicle);
            }
        }
    }
}
