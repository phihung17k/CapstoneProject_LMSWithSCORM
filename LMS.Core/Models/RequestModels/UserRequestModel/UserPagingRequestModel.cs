using LMS.Core.Enum;
using LMS.Core.Models.Common.RequestModels;

namespace LMS.Core.Models.RequestModels.UserRequestModel
{
    public class UserPagingRequestModel : PagingRequestModel
    {
        public string Search { get; set; }
        public bool? IsMale { get; set; }
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }
        public SortOrder? UserNameSort { get; set; }
        public int? ExceptRoleId { get; set; }
    }
}
