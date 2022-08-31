using LMS.Core.Enum;
using LMS.Core.Models.Common.RequestModels;

namespace LMS.Core.Models.RequestModels.RoleRequestModel
{
    public class RolePagingRequestModel : PagingRequestModel
    {
#nullable enable
        public string? Name { get; set; }
        public SortOrder? NameSort { get; set; }
    }
}