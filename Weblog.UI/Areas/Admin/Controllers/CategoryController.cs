using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Services;

namespace Weblog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }
        public IActionResult Create() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(!ModelState.IsValid) return View(category);

            await _categoryService.CreateCategoryAsync(category);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var category = _categoryService.GetCategoryByIdAsync(id);
            if(category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            await _categoryService.UpdateCategoryAsync(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}
