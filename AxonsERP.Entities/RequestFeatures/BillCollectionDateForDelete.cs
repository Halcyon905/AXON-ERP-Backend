using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.RequestFeatures
{
    public class BillCollectionDateForDelete
    {
        [Required]
        public string? customerCode { get; set; }
        [Required]
        public string? billColCalculate { get; set; }
        [Required]
        public string? departmentCode { get; set; }
        public int startDate { get; set; }
        public int endDate { get; set;}
    }
}