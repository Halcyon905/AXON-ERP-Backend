using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.RequestFeatures 
{
    public class TaxRateControlForDelete
    {
        [Required]
        public string? taxCode { get; set; }
        [Required]
        public DateTime effectiveDate { get; set; }
    }
}