using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.Models
{
    public class BillCollectionDate
    {
        public string? customerCode { get; set; }
        public string? nameLocal { get; set; }
        public string? nameEng { get; set; }
        public string? departmentCode { get; set; }
        public string? departmentDesc1 { get; set; }
        public string? departmentDesc2 { get; set; }
        public string? billColCalculate { get; set; }
        public string? billColCalculateDesc1 { get; set; }
        public string? billColCalculateDesc2 { get; set; }
        public int billCollectionDateMonthStart { get; set; }
        public int billCollectionDateMonthEnd { get; set; }
        public int billCollectionDateWeekStart { get; set; }
        public int billCollectionDateWeekEnd { get; set; }
    }
}