using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Services;

namespace Weblog.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostService _postService;
        private readonly CategoryService _categoryService;

        public HomeController(PostService postService, CategoryService categoryService)
        {
            _categoryService = categoryService;
            _postService = postService;
        }
        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> Index(string? searchTerm, Guid categoryId)
        {
            var posts = await _postService.SearchPostAsync(searchTerm, categoryId);
            var categories = await _categoryService.GetAllCategoriesAsync();

            ViewBag.Categories = categories;
            ViewBag.SerachTerm = searchTerm;
            ViewBag.CategoryId = categoryId;

            return View(posts);
        }
    }
}
