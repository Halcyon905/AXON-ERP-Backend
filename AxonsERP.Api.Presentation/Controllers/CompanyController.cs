using AxonsERP.Entities.ErrorModel;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AxonsERP.Api.Presentation 
{
    [Route("[controller]")]
    [ApiController]

    public class CompanyController : ControllerBase 
    {
        private readonly IServiceManager _service;
        public CompanyController(IServiceManager service) => _service = service;

        /// <summary>
        /// Gets all company information currently stored in the database.
        /// </summary>
        [HttpGet("GetListCompany")]
        [ProducesResponseType(typeof(IEnumerable<Company>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetListCompany()
        {
            var result = _service.CompanyService.GetListCompany();
            return Ok(result);
        }
        
        /// <summary>
        /// Searches company information based on given parameters.
        /// </summary>
        [HttpPost("SearchCompany")]
        [ProducesResponseType(typeof(IEnumerable<Company>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult SearchCompany(CompanyParameters parameters)
        {
            var result = _service.CompanyService.SearchCompany(parameters);
            return Ok(result);
        }
    }
}