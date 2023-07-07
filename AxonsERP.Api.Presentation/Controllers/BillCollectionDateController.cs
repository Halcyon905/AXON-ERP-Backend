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
        /// Gets a list of tax codes from general desc table
        /// </summary>
        [HttpGet("BillCollectionDateList")]
        [ProducesResponseType(typeof(IEnumerable<BillCollectionDateToReturn>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetBillCollectionDateList()
        {
            var billCollectionDateList = _service.BillCollectionDateService.GetListBillCollectionDate();
            return Ok(billCollectionDateList);
        }

        [HttpPost("CompanyBillCollectionDate")]
        [ProducesResponseType(typeof(IEnumerable<GeneralDesc>),200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public IActionResult GetCompanyBillCollectionDate([FromBody] BillCollectionDateForSingle billCollectionDateForSingle)
        {
            var companyBillCollectionDate = _service.BillCollectionDateService.GetCompanyBillCollectionDate(billCollectionDateForSingle);
            return Ok(companyBillCollectionDate);
        }
    }
}