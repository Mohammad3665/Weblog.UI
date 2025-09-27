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
        public async Task<IActionResult> Craete(Post post, IFormFile? Image)
        {
            if (!ModelState.IsValid) return View(post);

            if (Image != null && Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/PostImages");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                post.ImageUrl = "/Uploads/PostImages";
            }
            else
            {
                post.ImageUrl = "/Defaults/DefaultPostImage.png";
            }

            await _postService.CreatePostAsync(post);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(Guid id)
        {
            var post = _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (!ModelState.IsValid) return View(post);

            await _postService.UpdatePostAsync(post);
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
