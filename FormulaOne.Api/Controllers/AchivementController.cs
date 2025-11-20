using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Request;
using FormulaOne.Entities.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers
{
    [Route("api/achivement")]
    [ApiController]
    public class AchivementController : BaseController
    {
        public AchivementController(IUnitofWork unitofWork, IMapper mapper) : base(unitofWork, mapper)
        {
        }

        [HttpGet]
        [Route("{driverID:Guid}")]
        public async Task<IActionResult> GetDriverAchivements(Guid driverID)
        {
            Achivement? achivements = await _unitofWork.Achivements.GetAchivementsByDriverId(driverID); 
            if(achivements == null)
            {
                return NotFound("Achivement not found");
            }
            DriverAchivementResponse achivementDto = _mapper.Map<DriverAchivementResponse>(achivements);
            return Ok(achivementDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAchivement([FromBody]CreateDriverAchivementRequest achivement)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Achivement? result = _mapper.Map<Achivement>(achivement);

            await _unitofWork.Achivements.Add(result);
            bool isSaved = await _unitofWork.CompleteAsync();
            if (!isSaved)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating achivement");
            }
            return CreatedAtAction(nameof(GetDriverAchivements), new {driverId = result.DriverID}, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAchivement([FromBody]UpdateDriverAchivementRequest achivement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Achivement? result = _mapper.Map<Achivement>(achivement);
            await _unitofWork.Achivements.Update(result);
            bool isUpdated = await _unitofWork.CompleteAsync();
            if (!isUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating achivement");
            }
            return Ok(result);
        }
    }
}
