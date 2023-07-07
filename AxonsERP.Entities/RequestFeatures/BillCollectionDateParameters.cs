namespace AxonsERP.Entities.RequestFeatures 
{
    public class BillCollectionDateParameters : RequestParameters
    {
        public BillCollectionDateParameters() 
        {
            OrderBy = "customer_Code";
        }
    }

    public class BillCollectionDateForColumnSearchFilter
    {
        public string? customer_code { get; set; } = string.Empty;
        public string? department { get; set; } = string.Empty;
        public string? bill_col_calculate { get; set; } = string.Empty;
    }

    public class BillCollectionDateForColumnSearchTerm
    {
        public string? customer_code { get; set; } = string.Empty;
        public string? department { get; set; } = string.Empty;
        public string? bill_col_calculate { get; set; } = string.Empty;
    }
}