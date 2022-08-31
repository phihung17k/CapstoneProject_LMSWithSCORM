using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class UserRoleViewModel : UserViewModel
    {
        public List<RoleViewModelWithoutPermission> Roles { get; set; }
    }
}
