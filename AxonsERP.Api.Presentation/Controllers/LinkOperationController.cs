using AxonsERP.Entities.ErrorModel;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AxonsERP.Api.Presentation 
{
    [Route("[controller]")]
    [ApiController]

    public class LinkOperationController : ControllerBase 
    {
        private readonly IServiceManager _service;
        public LinkOperationController(IServiceManager service) => _service = service;

        /// <summary>
        /// Gets either operation or sub-operation codes from the database.
        /// </summary>
        [HttpGet("GetListLinkOperation")]
        [ProducesResponseType(typeof(IEnumerable<LinkOperation>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetListCompany(string mainColumn)
        {
            var result = _service.LinkOperationService.GetListLinkOperation(mainColumn);
            return Ok(result);
        }
        
        /// <summary>
        /// Searches link operation information based on given parameters.
        /// </summary>
        [HttpPost("SearchLinkOperation")]
        [ProducesResponseType(typeof(IEnumerable<LinkOperation>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult SearchCompany(LinkOperationParameters parameters)
        {
            var result = _service.LinkOperationService.SearchLinkOperation(parameters);
            return Ok(result);
        }
    }
}