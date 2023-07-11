using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.Models
{
    public class BillCollectionDateForGetSingle
    {
        public string? customerCode { get; set; }
        public string? billColCalculate { get; set; }
        public string? departmentCode { get; set; }
        public int dateOne { get; set; }
        public int dateTwo { get; set; }
    }
}