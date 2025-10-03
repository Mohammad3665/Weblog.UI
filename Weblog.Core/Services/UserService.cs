using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
using Weblog.Core.Domain.IdentityEntities;
using Weblog.Core.Domain.RepositoryContracts;

namespace Weblog.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        public async Task<ApplicationUser?> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }
        public async Task ActivateUserAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                user.LockoutEnd = null;
                await _userRepository.UpdateAsync(user);
            }
        }
        public async Task DeactiveUserAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user != null)
            {
                user.LockoutEnd = DateTimeOffset.MaxValue;
                await _userRepository.UpdateAsync(user);
            }
        }

        public int CountOfUsers()
        {
            return _userRepository.GetCountOfUsers();
        }
    }
}
