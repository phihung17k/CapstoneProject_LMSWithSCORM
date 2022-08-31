using LMS.Core.Models.Common.RequestModels;

namespace LMS.Core.Models.RequestModels
{
    public class SubjectPagingRequestModel : PagingRequestModel
    {
        public string Search { get; set; }
        public bool? IsActive { get; set; }
    }
}
