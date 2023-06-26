
namespace AxonsERP.Entities.Models
{
    public class TaxRateControl
    {
        public string TAX_CODE { get; set; }
        public DateTime EFFECTIVE_DATE { get; set; }
        public int RATE { get; set; }
        public string OWNER { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public string FUNCTION { get; set; }
        public int RATE_ORIGINAL { get; set; }
    }
}