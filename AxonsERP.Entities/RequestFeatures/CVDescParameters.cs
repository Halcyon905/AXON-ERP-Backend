namespace AxonsERP.Entities.RequestFeatures 
{
    public class CVDescParameters : RequestParameters
    {
        public CVDescParameters() 
        {
            OrderBy = "CV_Code";
        }
    }

    public class CVDescForColumnSearchFilter
    {
        public string? cv_Code { get; set; } = string.Empty;
        public string? name_Local { get; set; } = string.Empty;
        public string? name_Eng { get; set; } = string.Empty;
    }

    public class CVDescForColumnSearchTerm
    {
        public string? cv_Code { get; set; } = string.Empty;
        public string? name_Local { get; set; } = string.Empty;
        public string? name_Eng { get; set; } = string.Empty;
    }
}