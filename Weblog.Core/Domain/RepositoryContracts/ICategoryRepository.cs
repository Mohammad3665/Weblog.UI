using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;

namespace Weblog.Core.Domain.RepositoryContracts
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id);

        Task<List<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        int GetCategoriesCount();
    }
}
