using CongestionTaxApp.Interfaces;

namespace CongestionTaxApp.Models
{
    public static class VehicleFactory
    {
        public static IVehicle CreateVehicle(string vehicleType)
        {
            // Normalize the input to lowercase for case-insensitive matching
            return vehicleType.ToLower() switch
            {
                "car" => new Car(),
                "motorbike" => new Motorbike(),
                "bus" => new Bus(),
                "emergency" => new Emergency(),
                "diplomat" => new Diplomat(),
                "foreign" => new Foreign(),
                "military" => new Military(),
                _ => throw new ArgumentException("Invalid vehicle type")
            };
        }
    }
}
