namespace AxonsERP.Entities.RequestFeatures 
{
    public class TaxRateControlParameters : RequestParameters
    {
        public TaxRateControlParameters() 
        {
            OrderBy = "effectiveDate";
        }
    }

    public class TaxRateControlForColumnSearchFilter
    {
        public string? taxCode { get; set; } = string.Empty;
        public string? owner { get; set; } = string.Empty;
        public float rate { get; set; }
    }

    public class TaxRateControlForColumnSearchTerm
    {
        public string? taxCode { get; set; } = string.Empty;
        public string? owner { get; set; } = string.Empty;
        public float rate { get; set; }
    }
}