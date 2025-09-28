using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;

namespace Weblog.Core.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileStorageRepository _fileStorageRepository;

        public PostService(IPostRepository postRepository, ICategoryRepository categoryRepository, IFileStorageRepository fileStorageRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _fileStorageRepository = fileStorageRepository;
        }

        public async Task<Guid> CreatePostAsync(Post post, IFormFile? image)
        {
            
            await _postRepository.AddAsync(post, image);
            return post.Id;
        }
        public async Task UpdatePostAsync(Post post, IFormFile? newImage)
        {
            await _postRepository.UpdateAsync(post, newImage);
        }
        public async Task DeletePostAsync(Guid postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post != null)
            {
                await _postRepository.DeleteAsync(post);
            }
        }
        public async Task<List<Post>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return posts;
        }
        public async Task<Post?> GetPostByIdAsync(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            return post;
        }
        public async Task<List<Post>> SearchPostAsync(string? searchTerm, Guid? categoryId)
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
                posts = posts.Where(p => p.CategoryId == categoryId).ToList();
            }
            return posts;
        }
    }
}
