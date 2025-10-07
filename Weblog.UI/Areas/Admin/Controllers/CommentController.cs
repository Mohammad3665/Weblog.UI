using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Services;

namespace Weblog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<IActionResult> Index(Post post)
        {
            var comments = await _commentService.GetAllComments();
            return View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(Guid id)
        {
            await _commentService.ApproveCommentAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _commentService.DeleteCommentAsync(id);
            return RedirectToAction("Index");
        }
    }
}
