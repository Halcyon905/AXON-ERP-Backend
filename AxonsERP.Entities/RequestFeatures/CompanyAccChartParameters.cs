namespace AxonsERP.Entities.RequestFeatures 
{
    public class CompanyAccChartParameters : RequestParameters
    {
        public CompanyAccChartParameters() 
        {
            OrderBy = "ac_Code";
        }
    }

    public class CompanyAccChartForColumnSearchFilter
    {
        public string? ac_Code { get; set; } = string.Empty;
        public string? ac_Name_Local { get; set; } = string.Empty;
        public string? ac_Name_Eng { get; set; } = string.Empty;
    }

    public class CompanyAccChartForColumnSearchTerm
    {
        public string? ac_Code { get; set; } = string.Empty;
        public string? ac_Name_Local { get; set; } = string.Empty;
        public string? ac_Name_Eng { get; set; } = string.Empty;
    }
}