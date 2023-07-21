using AxonsERP.Entities.ErrorModel;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AxonsERP.Api.Presentation 
{
    [Route("[controller]")]
    [ApiController]

    public class TRNDescController : ControllerBase 
    {
        private readonly IServiceManager _service;
        public TRNDescController(IServiceManager service) => _service = service;

        /// <summary>
        /// Gets all company account information currently stored in the database.
        /// </summary>
        [HttpGet("GetListTRNDesc")]
        [ProducesResponseType(typeof(IEnumerable<TRNDesc>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetListTRNDesc()
        {
            var result = _service.TRNDescService.GetListTRNDesc();
            return Ok(result);
        }
        
        /// <summary>
        /// Searches company accounts based on given parameters.
        /// </summary>
        [HttpPost("SearchTRNDesc")]
        [ProducesResponseType(typeof(IEnumerable<TRNDesc>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult SearchTRNDesc(TRNDescParameters parameters)
        {
            var result = _service.TRNDescService.SearchTRNDesc(parameters);
            return Ok(result);
        }
    }
}