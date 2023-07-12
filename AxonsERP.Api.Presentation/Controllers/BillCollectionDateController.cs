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
        /// Gets a single bill collection date from database
        /// </summary>
        [HttpPost("SingleBillCollectionDate", Name = "GetSingleBillCollectionDate")]
        [ProducesResponseType(typeof(BillCollectionDateSingleToReturn),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult GetSingleBillCollectionDate(BillCollectionDateForGetSingle billCollectionDateForGetSingle)
        {
            var billCollectionDateList = _service.BillCollectionDateService.GetSingleBillCollectionDate(billCollectionDateForGetSingle);
            return Ok(billCollectionDateList);
        }

        /// <summary>
        /// Gets all bill collection dates for a particular company and bill collection code
        /// </summary>
        [HttpPost("CompanyBillCollectionDate")]
        [ProducesResponseType(typeof(BillCollectionDateToReturn),200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult GetCompanyBillCollectionDate([FromBody] BillCollectionDateForSingleCustomer billCollectionDateForSingle)
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
        /// Create a new bill collection date in edit page.
        /// </summary>
        [HttpPost("CreateBillCollectionDate")]
        [ProducesResponseType(typeof(BillCollectionDate),201)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult CreateBillCollectionDate(BillCollectionDateForCreate billCollectionDateForCreate) 
        {   
            var confirmation = _service.BillCollectionDateService.CreateBillCollectionDate(billCollectionDateForCreate);
            return CreatedAtRoute("GetSingleBillCollectionDate", confirmation);
        }

        /// <summary>
        /// Updates the dates on one bill collection date
        /// </summary>
        [HttpPut("UpdateBillCollectionDate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult UpdateBillCollectionDate([FromBody] BillCollectionDateForUpdate billCollectionDateForUpdate)
        {
            _service.BillCollectionDateService.UpdateBillCollectionDate(billCollectionDateForUpdate);
            return NoContent();
        }

        /// <summary>
        /// Delete all bill collection dates of client/company
        /// </summary>
        [HttpDelete("DeleteBillCollectionDateByCompany")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult DeleteBillCollectionDateByCompany([FromBody] IEnumerable<BillCollectionDateForDeleteMany> billCollectionDateForDeleteMany)
        {
            _service.BillCollectionDateService.DeleteBillCollectionDateByCompany(billCollectionDateForDeleteMany);
            return NoContent();
        }

        /// <summary>
        /// Delete all bill collection dates of a client/company
        /// </summary>
        [HttpDelete("DeleteBillCollectionDateByDate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult DeleteBillCollectionDateByDate([FromBody] BillCollectionDateForDelete billCollectionDateForDelete)
        {
            _service.BillCollectionDateService.DeleteBillCollectionDateByDate(billCollectionDateForDelete);
            return NoContent();
        }
    }
}