using System.Collections.Generic;
using System.ComponentModel;

namespace LMS.Core.Models.ViewModels
{
    public class RoleViewModel : RoleViewModelWithoutPermission
    {
        public string Description { get; set; }
        public List<PermissionRoleViewModel> Permissions { get; set; }
    }

    public class RoleViewModelWithoutPermission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool CanDelete { get; set; } = true; //role can be deleted or not
        public bool CanAssign { get; set; } = true; //role can be assign/unassign or not
        public bool CanDeactive { get; set; } = true; //role can be deactive or not
    }
}