using System.Collections.Generic;

namespace LMS.Core.Models.RequestModels.RoleRequestModel
{
    public class RoleCreateRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PermissionRoleRequestModel> Permissions { get; set; }
    }
}
