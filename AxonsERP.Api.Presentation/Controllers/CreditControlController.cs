using AxonsERP.Entities.ErrorModel;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AxonsERP.Api.Presentation 
{
    [Route("[controller]")]
    [ApiController]

    public class CreditControlController : ControllerBase 
    {
        private readonly IServiceManager _service;
        public CreditControlController(IServiceManager service) => _service = service;

        [HttpPost("GetSingleCreditControl")]
        [ProducesResponseType(typeof(CreditControl),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetSingleCreditControl(CreditControlForGetSingle creditControlForGetSingle)
        {
            var result = _service.CreditControlService.GetSingleCreditControl(creditControlForGetSingle);
            return Ok(result);
        }

        [HttpPost("SearchCreditControl")]
        [ProducesResponseType(typeof(CreditControl),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult SearchCreditControl(CreditControlParameters parameters)
        {
            var result = _service.CreditControlService.SearchCreditControl(parameters);
            return Ok(result);
        }
    }
}