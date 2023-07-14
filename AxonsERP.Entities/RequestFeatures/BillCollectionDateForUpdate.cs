using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.RequestFeatures
{
    public class BillCollectionDateForUpdate
    {
        [Required]
        public string? customerCode { get; set; }
        [Required]
        public string? billColCalculate { get; set; }
        [Required]
        public string? departmentCode { get; set; }
        [Required]
        public int startDate { get; set; }
        [Required]
        public int endDate { get; set;}
        public int newStartDate { get; set;}
        public int newEndDate { get; set; }
    }
}