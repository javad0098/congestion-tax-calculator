using CongestionTaxApp.Attributes;
using CongestionTaxApp.Interfaces;

namespace CongestionTaxApp.Helper
{
    public static class VehicleHelper
    {
        public static bool IsTollFreeVehicle(IVehicle vehicle)
        {
            var vehicleType = vehicle.GetType();
            return Attribute.IsDefined(vehicleType, typeof(TollFreeAttribute));
        }
    }
}
