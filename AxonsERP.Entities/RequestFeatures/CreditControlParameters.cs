namespace AxonsERP.Entities.RequestFeatures 
{
    public class CreditControlParameters : RequestParameters
    {
        public CreditControlParameters() 
        {
            OrderBy = "CUSTOMER_CODE";
        }
    }

    public class CreditControlForColumnSearchFilter
    {
        public string? customer_code { get; set; } = string.Empty;
        public string? department { get; set; } = string.Empty;
        public string? bill_col_calculate { get; set; } = string.Empty;
    }

    public class CreditControlForColumnSearchTerm
    {
        public string? customer_code { get; set; } = string.Empty;
        public string? department { get; set; } = string.Empty;
        public string? bill_col_calculate { get; set; } = string.Empty;
    }
}