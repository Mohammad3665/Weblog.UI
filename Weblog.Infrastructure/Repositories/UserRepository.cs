using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Domain.IdentityEntities;
using Weblog.Core.Domain.RepositoryContracts;
using Weblog.Infrastructure.DatabaseContext;

namespace Weblog.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WeblogDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(WeblogDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddAsync(ApplicationUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ApplicationUser user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public int GetCountOfUsers()
        {
            return _context.Users.Count();
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
