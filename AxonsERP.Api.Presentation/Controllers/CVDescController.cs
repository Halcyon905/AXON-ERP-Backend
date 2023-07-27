using AxonsERP.Entities.ErrorModel;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AxonsERP.Api.Presentation 
{
    [Route("[controller]")]
    [ApiController]

    public class CVDescController : ControllerBase 
    {
        private readonly IServiceManager _service;
        public CVDescController(IServiceManager service) => _service = service;

        /// <summary>
        /// Searches for any CV desc that matches the given parameters.
        /// </summary>
        [HttpPost("Search")]
        [ProducesResponseType(typeof(IEnumerable<GeneralDesc>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult SearchCVDesc(CVDescParameters parameters)
        {
            var searchResult = _service.CVDescService.SearchCustomerInfo(parameters);
            return Ok(searchResult);
        }
    }
}