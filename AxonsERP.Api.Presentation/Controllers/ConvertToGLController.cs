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
        [HttpPost("GetSingleConvertToGL", Name="GetSingleConvertToGL")]
        [ProducesResponseType(typeof(ConvertToGL),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetSingleConvertToGL(ConvertToGLForGetSingle convertToGLForGetSingle)
        {
            var result = _service.ConvertToGLService.GetSingleConvertToGL(convertToGLForGetSingle);
            return Ok(result);
        }

        /// <summary>
        /// Searches ConvertToGL information based on given parameters.
        /// </summary>
        [HttpPost("SearchConvertToGL")]
        [ProducesResponseType(typeof(ConvertToGL),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult SearchConvertToGL(ConvertToGLParameters parameters)
        {
            var result = _service.ConvertToGLService.SearchConvertToGL(parameters);
            return Ok(result);
        }

        /// <summary>
        /// Creates a single new ConvertToGL entry in the database.
        /// </summary>
        [HttpPost("CreateConvertToGL")]
        [ProducesResponseType(typeof(ConvertToGL),201)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult CreateConvertToGL(ConvertToGLForCreate convertToGLForCreate)
        {
            var confirmation = _service.ConvertToGLService.CreateConvertToGL(convertToGLForCreate);
            return CreatedAtRoute("GetSingleConvertToGL", confirmation);
        }

        /// <summary>
        /// Updates the account codes and debit or credit of a single ConvertToGL entry in the database.
        /// </summary>
        [HttpPut("UpdateConvertToGL")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult UpdateConvertToGL(ConvertToGLForUpdate convertToGLForUpdate)
        {
            _service.ConvertToGLService.UpdateConvertToGL(convertToGLForUpdate);
            return NoContent();
        }

        /// <summary>
        /// Deletes a single ConvertToGL entry in the database.
        /// </summary>
        [HttpDelete("DeleteManyConvertToGL")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult DeleteConvertToGL(List<ConvertToGLForGetSingle> convertToGLForGetSingle)
        {
            _service.ConvertToGLService.DeleteManyConvertToGL(convertToGLForGetSingle);
            return NoContent();
        }
    }
}