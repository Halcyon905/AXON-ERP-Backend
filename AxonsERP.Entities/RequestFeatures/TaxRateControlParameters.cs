namespace AxonsERP.Entities.RequestFeatures 
{
    public class TaxRateControlParameters : RequestParameters
    {
        public TaxRateControlParameters() 
        {
            OrderBy = "effective_Date";
        }
    }

    public class TaxRateControlForColumnSearchFilter
    {
        public string? tax_Code { get; set; } = string.Empty;
        public string? owner { get; set; } = string.Empty;
        public float rate { get; set; }
    }

    public class TaxRateControlForColumnSearchTerm
    {
        public string? tax_Code { get; set; } = string.Empty;
        public string? owner { get; set; } = string.Empty;
        public float rate { get; set; }
    }
}