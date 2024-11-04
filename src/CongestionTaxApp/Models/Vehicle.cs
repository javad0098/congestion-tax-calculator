using CongestionTaxApp.Attributes;
using CongestionTaxApp.Interfaces;

namespace CongestionTaxApp.Models
{
    public class Car : IVehicle
    {
        public string GetVehicleType() => "Car";
    }

    [TollFree]
    public class Motorbike : IVehicle
    {
        public string GetVehicleType() => "Motorbike";
    }

    [TollFree]
    public class Bus : IVehicle
    {
        public string GetVehicleType() => "Bus";
    }

    [TollFree]
    public class Emergency : IVehicle
    {
        public string GetVehicleType() => "Emergency";
    }

    [TollFree]
    public class Diplomat : IVehicle
    {
        public string GetVehicleType() => "Diplomat";
    }

    [TollFree]
    public class Foreign : IVehicle
    {
        public string GetVehicleType() => "Foreign";
    }

    [TollFree]
    public class Military : IVehicle
    {
        public string GetVehicleType() => "Military";
    }
}
