using Microsoft.AspNetCore.Identity;
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
        Task<IdentityUser?> GetByIdAsync(Guid id);

        Task<List<IdentityUser>> GetAllAsync();
        Task AddAsync(IdentityUser user);
        Task UpdateAsync(IdentityUser user);
        Task DeleteAsync(IdentityUser user);
    }
}
