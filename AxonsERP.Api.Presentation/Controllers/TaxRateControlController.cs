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
        /// date format = YYYY-MM-DDTHH:MI:SS e.g. 2018-08-01T00:00:00
        /// </summary>
        [HttpGet("{taxCode}/{effectiveDate}", Name = "GetTaxRateControl")]
        [ProducesResponseType(typeof(TaxRateControl),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetTaxRateControl(string taxCode, DateTime effectiveDate)
        {
            var taxRateControl = _service.TaxRateControlService.GetSingleTaxRateControl(taxCode, effectiveDate);
            if(taxRateControl == null) {
                throw new TaxRateControlNotFoundException(taxCode, effectiveDate);
            }
            return Ok(taxRateControl);
        }

        /// <summary>
        /// Gets all tax rate controls currently in database
        /// </summary>
        [HttpGet("TaxRateControlList")]
        [ProducesResponseType(typeof(IEnumerable<TaxRateControl>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult GetTaxRateControlList()
        {
            var taxRateControlList = _service.TaxRateControlService.GetListTaxRateControl();
            return Ok(taxRateControlList);
        }

        /// <summary>
        /// Create a new tax rate control
        /// </summary>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(TaxRateControl),201)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult CreateSingleTaxRateControl(TaxRateControlForCreate _taxRateControl) 
        {   
            var confirmation = _service.TaxRateControlService.CreateTaxRateControl(_taxRateControl);
            return CreatedAtRoute("GetTaxRateControl", new { taxCode = confirmation.taxCode, effectiveDate = confirmation.effectiveDate }, confirmation);
        }

        /// <summary>
        /// Change rate of a tax rate control
        /// </summary>
        [HttpPut("UpdateRate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult UpdateSingleTaxRateControl(TaxRateControlForUpdate _taxRateControl) 
        {   
            _service.TaxRateControlService.UpdateTaxRateControl(_taxRateControl);
            return NoContent();
        }
    }
}
