namespace AxonsERP.Entities.RequestFeatures 
{
    public class ConvertToGLForGetSingle
    {
        public string? company { get; set; }
        public string? operationCode { get; set; }
        public string? subOperation { get; set; }
        public string? businessType { get; set; }
        public string? groupAccount { get; set; }
        public string? docType { get; set; }
        public string? trnCode { get; set; }
        public DateTime effectiveDate { get; set; }
    }
}