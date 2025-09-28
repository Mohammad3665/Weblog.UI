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
        public PostController(PostService postService)
        {
            _postService = postService;
        }
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }
        public IActionResult Create() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Craete(Post post, IFormFile? image)
        {
            if (!ModelState.IsValid) return View(post);

            post.CreatedDate = DateTime.Now; 
            await _postService.CreatePostAsync(post, image);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(Guid id)
        {
            var post = _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post, IFormFile? newImage)
        {
            if (!ModelState.IsValid) return View(post);

            await _postService.UpdatePostAsync(post, newImage);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _postService.DeletePostAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
