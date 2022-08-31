using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.RoleRequestModel
{
    public class RoleUpdateRequestModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public List<PermissionRoleRequestModel> Permissions { get; set; }
    }
}
