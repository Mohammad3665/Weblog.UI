using Weblog.Core.Domain.Entities;
using Weblog.Core.Domain.RepositoryContracts;

public class CommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task AddCommentAsync(Comment comment)
    {
        await _commentRepository.AddAsync(comment);
    }

    public async Task ApproveCommentAsync(Guid commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        if (comment != null)
        {
            comment.IsApproved = true;
            await _commentRepository.UpdateAsync(comment);
        }
    }

    public async Task DeleteCommentAsync(Guid commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        if (comment != null)
        {
            await _commentRepository.DeleteAsync(comment);
        }
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
    {
        var comments = await _commentRepository.GetAllAsync();
        return comments.Where(c => c.PostId == postId).ToList();
    }

    public async Task<List<Comment>> GetAllComments()
    {
        var comments = await _commentRepository.GetAllAsync();
        return comments;
    }
}
