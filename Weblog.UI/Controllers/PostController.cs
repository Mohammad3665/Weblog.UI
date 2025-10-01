using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Services;

namespace Weblog.UI.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;
        private readonly CommentService _commentService;

        public PostController(PostService postService, CommentService commentService)
        {
            _commentService = commentService;
            _postService = postService;
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(Guid postId, string content)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                PostId = postId,
                Text = content,
                UserId = userId,
                CreatedDate = DateTime.Now,
                IsApproved = false
            };

            await _commentService.AddCommentAsync(comment);
            return RedirectToAction("Details", new { id = postId});
        }

    }
}
