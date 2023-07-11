using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.Models
{
    public class BillCollectionDateForUpdateDto
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
        public DateTime lastUpdateDate { get; set; }
        public string? function { get; set; }

    }
}