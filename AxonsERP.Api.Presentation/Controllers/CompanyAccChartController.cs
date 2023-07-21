using AxonsERP.Entities.ErrorModel;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AxonsERP.Api.Presentation 
{
    [Route("[controller]")]
    [ApiController]

    public class CompanyAccChartController : ControllerBase 
    {
        private readonly IServiceManager _service;
        public CompanyAccChartController(IServiceManager service) => _service = service;

        /// <summary>
        /// Gets all company account information currently stored in the database.
        /// </summary>
        [HttpGet("GetListCompanyAccChart")]
        [ProducesResponseType(typeof(IEnumerable<CompanyAccChart>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetListCompanyAccChart()
        {
            var result = _service.CompanyAccChartService.GetListCompanyAccChart();
            return Ok(result);
        }
    }
}