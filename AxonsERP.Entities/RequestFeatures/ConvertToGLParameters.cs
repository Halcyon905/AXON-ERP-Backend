namespace AxonsERP.Entities.RequestFeatures 
{
    public class ConvertToGLParameters : RequestParameters
    {
        public ConvertToGLParameters() 
        {
            OrderBy = "company";
        }
    }

    public class ConvertToGLForColumnSearchFilter
    {
        public string? doc_Type { get; set; } = string.Empty;
        public string? operation_Code { get; set; } = string.Empty;
        public string? sub_Operation { get; set; } = string.Empty;
        public string? business_Type { get; set; } = string.Empty;
        public string? account_Code1 { get; set; } = string.Empty;
        public string? account_Code2 { get; set; } = string.Empty;
    }

    public class ConvertToGLForColumnSearchTerm
    {
        public string? doc_Type { get; set; } = string.Empty;
        public string? operation_Code { get; set; } = string.Empty;
        public string? sub_Operation { get; set; } = string.Empty;
        public string? business_Type { get; set; } = string.Empty;
        public string? account_Code1 { get; set; } = string.Empty;
        public string? account_Code2 { get; set; } = string.Empty;
    }
}