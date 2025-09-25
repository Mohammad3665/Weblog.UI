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
        Task<Post?> GetByIdAsync(int id);

        Task<List<Post>> GetAllAsync();
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(Post post);
    }
}
