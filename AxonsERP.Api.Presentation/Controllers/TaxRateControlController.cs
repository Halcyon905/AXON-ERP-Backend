using AxonsERP.Entities.ErrorModel;
using AxonsERP.Entities.Exceptions;
using AxonsERP.Entities.RequestFeatures;
using AxonsERP.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AxonsERP.Api.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class TaxRateControl : ControllerBase
    {

        /// <summary>
        /// Gets the list of tax rate control
        /// </summary>
        [HttpPost("Search")]
        [ProducesResponseType(typeof(string),200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public IActionResult GetListBankDesc()
        {
            return Ok("Hello there");
        }
    }
}
