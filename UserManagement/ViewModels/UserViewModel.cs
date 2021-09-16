using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IEnumerable<String> Roles { get; set; }
    }
}
