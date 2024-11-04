namespace CongestionTaxApp.Features.CongestionTaxCalculator
{
    public class TaxRequest
    {
        public string VehicleType { get; set; }
        public List<string> Dates { get; set; }

        public (bool isValid, string errorMessage) IsValid()
        {
            if (string.IsNullOrWhiteSpace(VehicleType))
            {
                return (false, "VehicleType cannot be empty.");
            }

            if (Dates == null || Dates.Count == 0)
            {
                return (false, "At least one date must be provided.");
            }

            // Validate date format if necessary
            foreach (var date in Dates)
            {
                if (!DateTime.TryParse(date, out _))
                {
                    return (false, $"Invalid date format: {date}");
                }
            }

            return (true, null);
        }
    }
}