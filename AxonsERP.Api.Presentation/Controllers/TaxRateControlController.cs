using AxonsERP.Entities.ErrorModel;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.Exceptions;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AxonsERP.Api.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class TaxRateControlController : ControllerBase
    {
        private readonly IServiceManager _service;
        public TaxRateControlController(IServiceManager service) => _service = service;

        /// <summary>
        /// Gets a tax rate control
        /// </summary>
        [HttpGet("{taxCode}/{effectiveDate}")]
        [ProducesResponseType(typeof(TaxRateControl),200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult GetTaxRateControl(string taxCode, string effectiveDate)
        {
            var taxRateControl = _service.TaxRateControlService.GetSingleTaxRateControl(taxCode, effectiveDate);
            if(taxRateControl == null) {
                throw new TaxRateControlNotFoundException(taxCode, effectiveDate);
            }
            return Ok("Hello there");
        }

        [HttpGet("TaxRateControlList")]
        [ProducesResponseType(typeof(IEnumerable<TaxRateControl>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult GetTaxRateControlList()
        {
            var taxRateControlList = _service.TaxRateControlService.GetListTaxRateControl();
            return Ok(taxRateControlList);
        }
    }
}
