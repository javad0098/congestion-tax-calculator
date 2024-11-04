using CongestionTaxApp.Common;
using CongestionTaxApp.Configurations;
using CongestionTaxApp.Data;
using CongestionTaxApp.Features.CongestionTaxCalculator;


namespace CongestionTaxApp.Configurations
{
    public static class ServiceRegistry
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddTransient<ICongestionTaxCalculatorService, CongestionTaxCalculatorService>();
            services.AddSingleton<ILoggingService, LoggingService>();
            services.AddSingleton<ITollDataProvider, TollDataProvider>();

        }
    }
}
