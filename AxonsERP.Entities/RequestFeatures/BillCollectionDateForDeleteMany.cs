using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.RequestFeatures
{
    public class BillCollectionDateForDeleteMany
    {
        [Required]
        public string? customerCode { get; set; }
        [Required]
        public string? billColCalculate { get; set; }
        [Required]
        public string? departmentCode { get; set; }
    }
}