using AxonsERP.Entities.ErrorModel;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.Exceptions;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AxonsERP.Api.Presentation 
{
    [Route("[controller]")]
    [ApiController]

    public class GeneralDescController : ControllerBase 
    {
        private readonly IServiceManager _service;
        public GeneralDescController(IServiceManager service) => _service = service;

        /// <summary>
        /// Gets a list of tax codes from general desc table
        /// </summary>
        [HttpGet("GeneralDescList/{codeType}")]
        [ProducesResponseType(typeof(IEnumerable<GeneralDesc>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetGeneralDescList(string codeType)
        {
            var generalDescList = _service.GeneralDescService.GetListGeneralDesc(codeType);
            return Ok(generalDescList);
        }

        [HttpPost("Search")]
        [ProducesResponseType(typeof(IEnumerable<GeneralDesc>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult SearchGeneralDesc(GeneralDescParameters parameters)
        {
            var searchResult = _service.GeneralDescService.SearchGeneralDesc(parameters);
            return Ok(searchResult);
        }
    }
}