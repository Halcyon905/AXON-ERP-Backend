using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.DataTransferObjects 
{
    public class TaxRateControlDto 
    {
        [Required]
        public string? taxCode { get; set; }
        [Required]
        public DateTime effectiveDate { get; set; }

        public int rate { get; set; }
        public string? owner { get; set; }
        public DateTime createDate { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public string? function { get; set; }
        public int rateOriginal { get; set; }
    }
}