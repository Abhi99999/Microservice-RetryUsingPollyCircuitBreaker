using FormulaOne.AirlineService.Dto;

namespace FormulaOne.AirlineService.Services
{
    public interface ICalenderService
    {
        Task<List<FlightDto>> GetAvailableFlights();
    }
}
