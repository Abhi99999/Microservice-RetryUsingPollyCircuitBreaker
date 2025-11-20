using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Request;
using FormulaOne.Entities.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers
{
    [Route("api/driver")]
    [ApiController]
    public class DriverController : BaseController
    {
        public DriverController(IUnitofWork unitofWork, IMapper mapper) : base(unitofWork, mapper)
        {
        }
        [HttpGet]
        [Route("{driverID:Guid}")]
        public async Task<IActionResult> GetDriverById(Guid driverID)
        {
            var driver = await _unitofWork.Drivers.GetById(driverID);
            if (driver == null)
                return NotFound("Driver not found");
            
            GetDriverResponse driverDto = _mapper.Map<GetDriverResponse>(driver);
            return Ok(driverDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriver([FromBody] CreateDriverRequest driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Driver driverEntity = _mapper.Map<Driver>(driver);
            await _unitofWork.Drivers.Add(driverEntity);
            bool isSaved = await _unitofWork.CompleteAsync();
            if (!isSaved)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating driver");
            }
            return CreatedAtAction(nameof(GetDriverById), new { driverId = driverEntity.Id }, driverEntity);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDriver([FromBody] UpdateDriverRequest driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Driver driverEntity = _mapper.Map<Driver>(driver);
            await _unitofWork.Drivers.Update(driverEntity);
            bool isUpdated = await _unitofWork.CompleteAsync();
            if (!isUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating driver");
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _unitofWork.Drivers.All();
            var driverDtos = _mapper.Map<IEnumerable<GetDriverResponse>>(drivers);
            return Ok(driverDtos);
        }
        [HttpDelete]
        [Route("{driverID:Guid}")]
        public async Task<IActionResult> DeleteDriver(Guid driverID)
        {
            var driver = await _unitofWork.Drivers.GetById(driverID);
            if (driver == null)
            {
                return NotFound("Driver not found");
            }

            await _unitofWork.Drivers.Delete(driverID);
            bool isDeleted = await _unitofWork.CompleteAsync();
            if (!isDeleted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting driver");
            }
            return Ok($"Deleted Driver: {driverID}");
        }
    }
}
