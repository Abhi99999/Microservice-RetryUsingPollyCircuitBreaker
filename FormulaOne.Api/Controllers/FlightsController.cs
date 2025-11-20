using AutoMapper;
using FormulaOne.Api.Services.IServices;
using FormulaOne.DataService.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : BaseController
    {
        private readonly IFlightService _flightService;
        public FlightsController(IUnitofWork unitofWork, IMapper mapper, IFlightService flightService) : base(unitofWork, mapper)
        {
            _flightService = flightService;
        }
        [HttpGet]
        public async Task<IActionResult> GetFlights()
        {
            try
            {
                var flights = await _flightService.GetAvailableFlights();
                return Ok(flights);
            }
            catch (Exception ex)
            {
                return BadRequest( $"Error retrieving flights: {ex.Message}");
            }
        }
    }
}
