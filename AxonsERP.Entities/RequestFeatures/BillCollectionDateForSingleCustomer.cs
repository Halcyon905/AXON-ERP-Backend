using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.RequestFeatures
{
    public class BillCollectionDateForSingleCustomer
    {
        public string? customerCode { get; set; }
        public string? billColCalculate { get; set; }
        public string? departmentCode { get; set; } 
    }
}