namespace AxonsERP.Entities.RequestFeatures 
{
    public class GeneralDescParameters : RequestParameters
    {
        public string? codeType { get; set; }
        public GeneralDescParameters() 
        {
            OrderBy = "gdCode";
        }
    }

    public class GeneralDescForColumnSearchFilter
    {
        public string? gdCode { get; set; } = string.Empty;
        public string? desc1 { get; set; } = string.Empty;
        public string? desc2 { get; set; } = string.Empty;
    }

    public class GeneralDescForColumnSearchTerm
    {
        public string? gdCode { get; set; } = string.Empty;
        public string? desc1 { get; set; } = string.Empty;
        public string? desc2 { get; set; } = string.Empty;
    }
}