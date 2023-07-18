using AxonsERP.Entities.ErrorModel;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AxonsERP.Api.Presentation 
{
    [Route("[controller]")]
    [ApiController]

    public class ConvertToGLController : ControllerBase 
    {
        private readonly IServiceManager _service;
        public ConvertToGLController(IServiceManager service) => _service = service;

        /// <summary>
        /// Gets all ConvertToGL information in database.
        /// </summary>
        [HttpGet("GetListConvertToGL")]
        [ProducesResponseType(typeof(IEnumerable<ConvertToGL>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetListConvertToGL()
        {
            var result = _service.ConvertToGLService.GetListConvertToGL();
            return Ok(result);
        }

        /// <summary>
        /// Gets a single ConvertToGL information from database.
        /// </summary>
        [HttpPost("GetSingleConvertToGL")]
        [ProducesResponseType(typeof(ConvertToGL),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetSingleConvertToGL(ConvertToGLForGetSingle convertToGLForGetSingle)
        {
            var result = _service.ConvertToGLService.GetSingleConvertToGL(convertToGLForGetSingle);
            return Ok(result);
        }
    }
}