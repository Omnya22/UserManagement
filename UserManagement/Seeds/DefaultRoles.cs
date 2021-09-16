using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Constants;

namespace UserManagement.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.BasicUser.ToString()));
            }
        }
    }
}
