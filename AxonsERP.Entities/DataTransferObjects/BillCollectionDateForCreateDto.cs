using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.Models
{
    public class BillCollectionDateForCreateDto
    {
        public string? customerCode { get; set; }
        public string? departmentCode { get; set; }
        public string? billColCalculate { get; set; }
        public int startDate { get; set; }
        public int endDate { get; set; }
        public int weekNo { get; set; }
        public int dayOfWeek { get; set; }
        public string? owner { get; set; }
        public DateTime createDate { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public string? function { get; set; }
    }
}