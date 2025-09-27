using Microsoft.AspNetCore.Identity;
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
    public class UserRepository : IUserRepository
    {
        private readonly WeblogDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(WeblogDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddAsync(IdentityUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IdentityUser user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<IdentityUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IdentityUser?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(IdentityUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
