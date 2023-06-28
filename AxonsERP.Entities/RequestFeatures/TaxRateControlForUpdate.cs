using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.RequestFeatures
{
    public class TaxRateControlForUpdate 
    {
        [Required]
        public string? taxCode { get; set; }
        [Required]
        public DateTime effectiveDate { get; set; }

        public int rate { get; set; }
        public int rateOriginal { get; set; }
    }
}