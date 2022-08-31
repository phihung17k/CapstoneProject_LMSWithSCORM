namespace LMS.Core.Models.Common.RequestModels
{
    public class PagingRequestModel
    {
        const int MaxPageSize = 50;
        //[FromQuery(Name = "current-page")]
        public int CurrentPage { get; set; } = 1;
        private int _pageSize = 10;
        //[FromQuery(Name = "page-size")]
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
    }
}
