using Microsoft.AspNetCore.Mvc;
using Weblog.Core.Services;

namespace Weblog.UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostService _postService;
        private readonly UserService _userService;
        private readonly CategoryService _categoryService;
        public HomeController(PostService postService, CategoryService categoryService, UserService userService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            var allOfPosts = _postService.CountOfPosts();
            ViewBag.AllOfPosts = allOfPosts;

            var allOfCategories = _categoryService.CountOfCategories();
            ViewBag.AllOfCategories = allOfCategories;

            var allOfUsers = _userService.CountOfUsers();
            ViewBag.AllOfUsers = allOfUsers;

            return View();
        }
    }
}
