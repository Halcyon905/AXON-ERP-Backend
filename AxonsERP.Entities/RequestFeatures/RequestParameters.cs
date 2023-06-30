namespace AxonsERP.Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int MaxPageSize = 99999999;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
        public List<Search>? Search { get; set; }
        public string? SearchTermAlias { get; set; }
        public string? SearchTermName { get; set; }
        public string? SearchTermValue { get; set; }
        public string? OrderBy { get; set; }
    }
}
