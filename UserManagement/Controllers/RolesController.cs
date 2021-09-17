using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagement.Constants;
using UserManagement.Models;
using UserManagement.ViewModels;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", await _roleManager.Roles.ToListAsync());

            if (await _roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "Role is exists!");
                return View("Index", await _roleManager.Roles.ToListAsync());
            }

            await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));

            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> ManagePermissions(string roleId)
        //{
        //    var role = await _roleManager.FindByIdAsync(roleId);

        //    if (role == null)
        //        return NotFound();

        //    var roleClaims = _roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();
        //    var allClaims = Permissions.GeneratePermissions();
        //    var allPermissions = allClaims.Select(p => new RolesManagedViewModel { RoleName = p }).ToList();

        //    foreach (var permission in allPermissions)
        //    {
        //        if (roleClaims.Any(c => c == permission.RoleName))
        //            permission.IsSelected = true;
        //    }

        //    var viewModel = new PermissionsFormViewModel
        //    {
        //        RoleId = roleId,
        //        RoleName = role.Name,
        //        RoleCalims = allPermissions
        //    };

        //    return View(viewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ManagePermissions(PermissionsFormViewModel model)
        //{
        //    var role = await _roleManager.FindByIdAsync(model.RoleId);

        //    if (role == null)
        //        return NotFound();

        //    var roleClaims = await _roleManager.GetClaimsAsync(role);

        //    foreach (var claim in roleClaims)
        //        await _roleManager.RemoveClaimAsync(role, claim);

        //    var selectedClaims = model.RoleCalims.Where(c => c.IsSelected).ToList();

        //    foreach (var claim in selectedClaims)
        //        await _roleManager.AddClaimAsync(role, new Claim("Permission", claim.RoleName));

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
