using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;

namespace Weblog.Core.Domain.RepositoryContracts
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);

        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
