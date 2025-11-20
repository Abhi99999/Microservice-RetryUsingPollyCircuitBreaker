using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers
{
    [Route("api/base")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitofWork _unitofWork;
        protected readonly IMapper _mapper;

        public BaseController(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }
    }
}
