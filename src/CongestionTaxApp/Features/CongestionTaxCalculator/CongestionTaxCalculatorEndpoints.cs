using CongestionTaxApp.Common;
using CongestionTaxApp.Interfaces;
using CongestionTaxApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxApp.Features.CongestionTaxCalculator
{
    public static class CongestionTaxCalculatorEndPoints
    {
        public static void MapCongestionTax(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/CongestionTax/calculate", async (
                [FromServices] ICongestionTaxCalculatorService service,
                [FromBody] TaxRequest request,
                [FromServices] ILoggingService logger) =>
            {
                // Validate the request
                var (isValid, errorMessage) = request.IsValid();
                if (!isValid)
                {
                    logger.LogWarning("Validation failed: {ErrorMessage}", errorMessage);
                    return ApiResponseHelper.HandleApiResponse(
                        ApiResponse<int>.ErrorResponse(errorMessage, ApiErrorType.ValidationError));
                }

                try
                {
                    logger.LogInformation("Processing tax calculation for vehicle type: {VehicleType}", request.VehicleType);

                    IVehicle vehicle = VehicleFactory.CreateVehicle(request.VehicleType);
                    DateTime[] dates = request.Dates.Select(DateTime.Parse).ToArray();
                    
                    int tax = await service.GetTaxAsync(vehicle, dates);

                    logger.LogInformation("Tax calculation completed successfully: {Tax}", tax);
                    return ApiResponseHelper.HandleApiResponse(ApiResponse<int>.SuccessResponse(tax));
                }
                catch (ArgumentException ex)
                {
                    logger.LogError("Argument exception occurred while processing the request.",ex);
                    return ApiResponseHelper.HandleApiResponse(
                        ApiResponse<int>.ErrorResponse(ex.Message, ApiErrorType.ValidationError));
                }
                catch (Exception ex)
                {
                    logger.LogError( "An unexpected error occurred while processing the request.",ex);
                    return ApiResponseHelper.HandleApiResponse(
                        ApiResponse<int>.ErrorResponse("An error occurred while processing the request.", ApiErrorType.ServerError));
                }
            })
            .WithOpenApi()
            .WithTags("CongestionTax");
        }
    }
}
