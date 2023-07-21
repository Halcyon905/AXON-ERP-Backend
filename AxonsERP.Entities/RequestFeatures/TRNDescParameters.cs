namespace AxonsERP.Entities.RequestFeatures 
{
    public class TRNDescParameters : RequestParameters
    {
        public TRNDescParameters() 
        {
            OrderBy = "doc_Type";
        }
    }

    public class TRNDescForColumnSearchFilter
    {
        public string? doc_type { get; set; } = string.Empty;
        public string? trn_Code { get; set; } = string.Empty;
        public string? name_Local { get; set; } = string.Empty;
        public string? name_Eng { get; set; } = string.Empty;
    }

    public class TRNDescForColumnSearchTerm
    {
        public string? doc_type { get; set; } = string.Empty;
        public string? trn_Code { get; set; } = string.Empty;
        public string? name_Local { get; set; } = string.Empty;
        public string? name_Eng { get; set; } = string.Empty;
    }
}