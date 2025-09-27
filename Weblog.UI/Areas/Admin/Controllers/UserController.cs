using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Weblog.Core.Domain.IdentityEntities;

namespace Weblog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        public async Task<IActionResult> ManageRoles(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            ViewBag.Roles = roles;
            ViewBag.UserRoles = userRoles;

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(Guid id, string role)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            await _userManager.AddToRoleAsync(user, role);
            return RedirectToAction("ManageRoles", new { id = id});
        }

        [HttpPost]
        public async Task<ActionResult> DeleteRole(Guid id, string role)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            await _userManager.RemoveFromRoleAsync(user, role);
            return RedirectToAction("ManageRoles", new {id = id});
        }

        [HttpPost]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            if(user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow) user.LockoutEnd = null;

            else user.LockoutEnd = DateTimeOffset.MaxValue;

            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
    }
}
