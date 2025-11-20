using FormulaOne.AirlineService.Dto;

namespace FormulaOne.AirlineService.Services
{
    public class CalenderService : ICalenderService
    {
        private DateTime _recoveryTime = DateTime.UtcNow;
        private static readonly Random _random = new();
        public Task<List<FlightDto>> GetAvailableFlights()
        {
            if(_recoveryTime > DateTime.UtcNow)
                throw new Exception("Service is in recovery mode. Please try again later.");

            if(_recoveryTime < DateTime.UtcNow && _random.Next(1, 5) ==  1) 
            {
                _recoveryTime = DateTime.UtcNow.AddSeconds(20);
            }
            var flights = new List<FlightDto>
            {
                new() { FlightNumber = "F101", Departure = "New York", Arrival = "London", Price = 10000, FlightDate = DateTime.UtcNow.AddDays(3) },
                new() { FlightNumber = "F202", Departure = "Paris", Arrival = "Tokyo", Price = 14000, FlightDate = DateTime.UtcNow.AddDays(18) },
                new() { FlightNumber = "F303", Departure = "Sydney", Arrival = "Dubai",  Price = 18000, FlightDate = DateTime.UtcNow.AddDays(20) }
            };
            return Task.FromResult(flights);
        }
    }
}
