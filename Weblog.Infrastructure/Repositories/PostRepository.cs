using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Domain.RepositoryContracts;
using Weblog.Infrastructure.DatabaseContext;

namespace Weblog.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly WeblogDbContext _context;
        private readonly IFileStorageRepository _fileStorageRepository;

        public PostRepository(WeblogDbContext context, IFileStorageRepository fileStorageRepository)
        {
            _context = context;
            _fileStorageRepository = fileStorageRepository;
        }

        public async Task AddAsync(Post post , IFormFile? Image)
        {
            if (Image != null)
            {
                post.ImageUrl = await _fileStorageRepository.SaveFileAsync(Image, "uploads/thumbnails");
            }
            post.CreatedDate = DateTime.Now;
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _context.Posts
            .Include(p => p.Category)
            .Include(p => p.Comments)
            .ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(Guid id)
        {
            return await _context.Posts
            .Include(p => p.Category)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task UpdateAsync(Post post, IFormFile? newImage)
        {
            if (newImage != null)
            {
                if (!string.IsNullOrEmpty(post.ImageUrl))
                    await _fileStorageRepository.DeleteFileAsync(post.ImageUrl, "wwwroot/Uploads/PostImages");

                post.ImageUrl = await _fileStorageRepository.SaveFileAsync(newImage, "wwwroot/Uploads/PostImages");
            }
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
        
    }
}
