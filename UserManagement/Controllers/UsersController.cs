using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.ViewModels;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
                            .Select(user => new UserViewModel
                            {
                                Id = user.Id,
                                FName = user.FName,
                                LName = user.LName,
                                UserName = user.UserName,
                                Email = user.Email,
                                Roles = _userManager.GetRolesAsync(user).Result
                            }).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> ManageRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var model = new FormViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                Items = roles.Select(role => new CheckBoxViewModel
                {
                    Value = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRoles(FormViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, model.Items.Where(r => r.IsSelected).Select(r => r.Value));

            return RedirectToAction(nameof(Index));
        }
    }
}
