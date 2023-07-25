namespace AxonsERP.Entities.RequestFeatures 
{
    public class LinkOperationParameters : RequestParameters
    {
        public string? mainColumn { get; set; } = string.Empty;
        public LinkOperationParameters() 
        {
            OrderBy = "company";
        }
    }

    public class LinkOperationForColumnSearchFilter
    {
        public string? company { get; set; } = string.Empty;
        public string? operation_code { get; set; } = string.Empty;
        public string? sub_operation { get; set; } = string.Empty;
    }

    public class LinkOperationForColumnSearchTerm
    {
        public string? company { get; set; } = string.Empty;
        public string? operation_code { get; set; } = string.Empty;
        public string? sub_operation { get; set; } = string.Empty;
    }
}