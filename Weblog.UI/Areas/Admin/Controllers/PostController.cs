using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Services;

namespace Weblog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private readonly PostService _postService;
        private readonly CommentService _commentService;
        private readonly CategoryService _categoryService;
        public PostController(PostService postService, CommentService commentService, CategoryService categoryService)
        {
            _postService = postService;
            _commentService = commentService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post, IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(post);
            }

            post.CreatedDate = DateTime.Now; 
            await _postService.CreatePostAsync(post, image, post.AuthorName);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post, IFormFile? newImage)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(post);
            }

            await _postService.UpdatePostAsync(post, newImage);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _postService.DeletePostAsync(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> GetComments(Guid id)
        {
            var comments = await _postService.GetPostCommentsAsync(id);
            return View();
        }

    }
}
