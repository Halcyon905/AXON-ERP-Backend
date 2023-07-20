namespace AxonsERP.Entities.RequestFeatures
{
    public class ConvertToGLForCreate {
        public string? company { get; set; }
        public string? operationCode { get; set; }
        public string? subOperation { get; set; }
        public string? businessType { get; set; }
        public string? groupAccount { get; set; }
        public string? docType { get; set; }
        public string? trnCode { get; set; }
        public DateTime effectiveDate { get; set; }
        public string? postFlag { get; set; }
        public string? accountCode1 { get; set; }
        public string? accountCode2 { get; set; }
        public string? type1 { get; set; }
        public string? type2 { get; set; }
        public string? owner { get; set; }
        public DateTime createDate { get; set; }
        public DateTime lastUpdateDate { get; set; }
    }
}