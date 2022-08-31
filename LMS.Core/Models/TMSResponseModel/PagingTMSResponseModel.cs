using Newtonsoft.Json;

namespace LMS.Core.Models.TMSResponseModel
{
    public class PagingTMSResponseModel
    {
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("totalPage")]
        public int TotalPage { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("hasPrevious")]
        public bool HasPrevious { get; set; }

        [JsonProperty("hasNext")]
        public bool HasNext { get; set; }
    }
}
