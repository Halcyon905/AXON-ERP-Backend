using System.ComponentModel.DataAnnotations;

namespace AxonsERP.Entities.Models
{
    public class BillCollectionDateForCreate
    {
        public string? customerCode { get; set; }
        public string? departmentCode { get; set; }
        public string? billColCalculate { get; set; }
        public int startDate 
        { 
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
            } 
        }
        public int endDate
        { 
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            } 
        }
        public int weekNo
        { 
            get
            {
                return _weekNo;
            }
            set
            {
                _weekNo = value;
            } 
        }
        public int dayOfWeek
        { 
            get
            {
                return _dayOfWeek;
            }
            set
            {
                _dayOfWeek = value;
            } 
        }
        public string? owner { get; set; }
        private int _startDate = 0;
        private int _endDate = 0;
        private int _weekNo = 0;
        private int _dayOfWeek = 0;
    }
}