using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;

namespace Weblog.Core.Domain.RepositoryContracts
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(Guid id);

        Task<List<Post>> GetAllAsync();
        Task AddAsync(Post post, IFormFile? image, string authorName);
        Task UpdateAsync(Post post, IFormFile? newImage);
        Task DeleteAsync(Post post);
    }
}
