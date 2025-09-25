using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Domain.RepositoryContracts;

namespace Weblog.Core.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;

        public PostService(IPostRepository postRepository, ICommentRepository commentRepository, ICategoryRepository categoryRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<string> CreatePostAsync(Post post, Comment comment)
        {
            await _postRepository.AddAsync(post);
            return post.Id;
        }
        public async Task UpdatePostAsync(Post post)
        {
            await _postRepository.UpdateAsync(post);
        }
        public async Task DeletePostAsync(string postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post != null)
            {
                var comments = await _commentRepository.GetAllAsync();
                foreach (var comment in comments.Where(c => c.PostId == postId))
                {
                    await _postRepository.DeleteAsync(post);
                }
                await _postRepository.DeleteAsync(post);
            }
        }
        public async Task<List<Post>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return posts;
        }
        public async Task<Post?> GetPostByIdAsync(string id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            return post;
        }
        public async Task<List<Post>> SearchPostAsync(string? searchTerm, string? categoryId)
        {
            var posts = await _postRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                posts = posts.Where(p => p.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || 
                p.Content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
            }
            if (categoryId != null)
            {
                posts = posts.Where(p => p.CategoryId == categoryId.ToString()).ToList();
            }
            return posts;
        }
    }
}
