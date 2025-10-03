using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Domain.IdentityEntities;

namespace Weblog.Core.Domain.RepositoryContracts
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(Guid id);

        Task<List<ApplicationUser>> GetAllAsync();
        Task AddAsync(ApplicationUser user);
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(ApplicationUser user);

        int GetCountOfUsers();
    }
}
