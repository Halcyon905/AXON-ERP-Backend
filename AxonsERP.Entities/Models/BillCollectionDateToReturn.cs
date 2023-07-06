using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.Models
{
    public class BillCollectionDateToReturn
    {
        public string? customerCode { get; set; }
        public string? nameLocal { get; set; }
        public string? nameEng { get; set; }
        public string? department { get; set; }
        public string? departmentDesc1 { get; set; }
        public string? departmentDesc2 { get; set; }
        public string? billColCalculate { get; set; }
        public string? billColCalculateDesc1 { get; set; }
        public string? billColCalculateDesc2 { get; set; }
        public IEnumerable<int> billCollectionDateStart { get; set; }
        public IEnumerable<int> billCollectionDateEnd { get; set; }
    }
}