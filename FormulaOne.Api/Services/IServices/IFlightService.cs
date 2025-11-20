using FormulaOn.Api.Dto;

namespace FormulaOne.Api.Services.IServices
{
    public interface IFlightService
    {
        Task<List<FlightDto>> GetAvailableFlights();
    }
}
