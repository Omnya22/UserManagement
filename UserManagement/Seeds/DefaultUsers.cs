using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagement.Constants;
using UserManagement.Models;

namespace UserManagement.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedBasicUserAsync(UserManager<AppUser> userManager)
        {
            var defaultuser = new AppUser
            {
                 FName = "User",
                 LName = "User",
                 UserName = "User",
                 Email = "user@basicuser.com"
            };

            var user = await userManager.FindByEmailAsync(defaultuser.Email);
            if(user == null)
            {
                await userManager.CreateAsync(defaultuser, "123456");
                await userManager.AddToRoleAsync(defaultuser,Roles.BasicUser.ToString());
            }
        }

        public static async Task SeedAdminUserAsync(UserManager<AppUser> userManager)
        {
            var defaultuser = new AppUser
            {
                FName = "Admin",
                LName = "Admin",
                UserName = "AdminUser",
                Email = "admin@user.com"
            };

            var user = await userManager.FindByEmailAsync(defaultuser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultuser, "123456");
                await userManager.AddToRoleAsync(defaultuser,Roles.Admin.ToString());
            }
        }

        public static async Task SeedSuperAdminUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultuser = new AppUser
            {
                FName = "SuperAdmin",
                LName = "SuperAdmin",
                UserName = "SuperAdminUser",
                Email = "superadmin@user.com"
            };

            var user = await userManager.FindByEmailAsync(defaultuser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultuser, "123456");
                await userManager.AddToRolesAsync(defaultuser,new List<string> { Roles.BasicUser.ToString(), Roles.Admin.ToString(), Roles.SuperAdmin.ToString() });
            }

            await roleManager.SeedClaimsForSuperAdminUser();
        }

        private static async Task SeedClaimsForSuperAdminUser(this RoleManager<IdentityRole> roleManager)
        {
            var adminrole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            await roleManager.AddPermissionToClaims(adminrole, Modules.Product.ToString());
        }

        public static async Task AddPermissionToClaims(this RoleManager<IdentityRole> roleManager,IdentityRole role,string module)
        {
            var allclaims = await roleManager.GetClaimsAsync(role);
            var allpermission = Permissions.GeneratePermissions(module);

            foreach (var permission in allpermission)
            {
                if (!allclaims.Any(c => c.Type == "Permission" && c.Value == permission))
                    await roleManager.AddClaimAsync(role,new Claim("Permission", permission));
            }
        }
    }
}
