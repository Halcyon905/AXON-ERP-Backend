namespace AxonsERP.Entities.RequestFeatures 
{
    public class CompanyParameters : RequestParameters
    {
        public CompanyParameters() 
        {
            OrderBy = "company";
        }
    }

    public class CompanyForColumnSearchFilter
    {
        public string? company { get; set; } = string.Empty;
        public string? name_Local { get; set; } = string.Empty;
        public string? name_Eng { get; set; } = string.Empty;
    }

    public class CompanyForColumnSearchTerm
    {
        public string? company { get; set; } = string.Empty;
        public string? name_Local { get; set; } = string.Empty;
        public string? name_Eng { get; set; } = string.Empty;
    }
}