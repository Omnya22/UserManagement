using System.Collections.Generic;

namespace UserManagement.ViewModels
{
    public class PermissionsFormViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RolesManagedViewModel> RoleCalims { get; set; }
    }
}
