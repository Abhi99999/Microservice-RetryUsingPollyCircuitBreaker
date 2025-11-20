using FormulaOne.AirlineService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.AirlineService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsCalenderController : ControllerBase
    {
        private readonly ILogger<FlightsCalenderController> _logger;
        private readonly ICalenderService _calenderService;
        public FlightsCalenderController(ILogger<FlightsCalenderController> logger, ICalenderService calenderService)
        {
            _logger = logger;
            _calenderService = calenderService;
        }
        [HttpGet("GetFlightCalender")]
        public async Task<IActionResult> GetFlightCalender()
        {
            try
            {
                _logger.LogInformation("GetFlightCalender called");
                var flights = await _calenderService.GetAvailableFlights();
                return Ok(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFlightCalender");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
