
namespace AxonsERP.Entities.Models
{
    public class TaxRateControl
    {
        public string? taxCode { get; set; }
        public DateTime effectiveDate { get; set; }
        public float rate { get; set; }
        public string? owner { get; set; }
        public DateTime createDate { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public string? function { get; set; }
        public float rateOriginal { get; set; }
        public string? desc1 { get; set; }
        public string? desc2 { get; set; }
    }
}