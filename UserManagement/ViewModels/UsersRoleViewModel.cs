using System.Collections.Generic;

namespace UserManagement.ViewModels
{
    public class UsersRoleViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public List<RolesManagedViewModel> Roles { get; set; }
    }
}
