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

    public class BillCollectionDateController : ControllerBase 
    {
        private readonly IServiceManager _service;
        public BillCollectionDateController(IServiceManager service) => _service = service;

        /// <summary>
        /// Gets all bill collection dates from database
        /// </summary>
        [HttpGet("BillCollectionDateList")]
        [ProducesResponseType(typeof(IEnumerable<BillCollectionDateToReturn>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult GetBillCollectionDateList()
        {
            var billCollectionDateList = _service.BillCollectionDateService.GetListBillCollectionDate();
            return Ok(billCollectionDateList);
        }

        /// <summary>
        /// Gets all bill collection dates for a particular company and bill collection code
        /// </summary>
        [HttpPost("CompanyBillCollectionDate")]
        [ProducesResponseType(typeof(BillCollectionDateToReturn),200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult GetCompanyBillCollectionDate([FromBody] BillCollectionDateForSingle billCollectionDateForSingle)
        {
            var companyBillCollectionDate = _service.BillCollectionDateService.GetCompanyBillCollectionDate(billCollectionDateForSingle);
            return Ok(companyBillCollectionDate);
        }

        /// <summary>
        /// Searches all bill collection dates based on given parameters
        /// </summary>
        [HttpPost("SearchBillCollectionDate")]
        [ProducesResponseType(typeof(IEnumerable<BillCollectionDateToReturn>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult SearchBillCollectionDate([FromBody] BillCollectionDateParameters parameters)
        {
            var billCollectionDateList = _service.BillCollectionDateService.SearchBillCollectionDate(parameters);
            return Ok(billCollectionDateList);
        }

        /// <summary>
        /// Updates the dates on one bill collection date
        /// </summary>
        [HttpPut("UpdateBillCollectionDate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult UpdateBillCollectionDate([FromBody] BillCollectionDateForUpdate billCollectionDateForUpdate)
        {
            _service.BillCollectionDateService.UpdateBillCollectionDate(billCollectionDateForUpdate);
            return NoContent();
        }
    }
}