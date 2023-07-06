using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.Models
{
    public class BillCollectionDateForSingle
    {
        public string? customerCode { get; set; }
        public string? billColCalculate { get; set; }
        public string? departmentCode { get; set; } 
    }
}