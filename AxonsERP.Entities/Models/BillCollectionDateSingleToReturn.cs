using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.Models
{
    public class BillCollectionDateSingleToReturn
    {
        public string? customerCode { get; set; }
        public string? departmentCode { get; set; }
        public string? billColCalculate { get; set; }
        public int startDate { get; set; }
        public int endDate { get; set; }
        public int weekNo { get; set; }
        public int dayOfWeek { get; set; }
    }
}