using CongestionTaxApp.Features.CongestionTaxCalculator;

namespace CongestionTaxApp.Configurations
{
    public static class EndpointMapper
    {
        public static void MapAllEndpoints(WebApplication app)
        {
            app.MapCongestionTax();
        }
    }
}
