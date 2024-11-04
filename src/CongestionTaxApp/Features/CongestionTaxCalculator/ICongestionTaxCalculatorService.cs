using System;
using System.Threading.Tasks;
using CongestionTaxApp.Interfaces;

namespace CongestionTaxApp.Features.CongestionTaxCalculator
{
    public interface ICongestionTaxCalculatorService
    {
        Task<int> GetTaxAsync(IVehicle vehicle, DateTime[] dates);
    }
}