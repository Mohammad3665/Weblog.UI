using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Domain.Entities;
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

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        public async Task<User?> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }
        public async Task ActivateUserAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                user.IsActive = true;
                await _userRepository.UpdateAsync(user);
            }
        }
        public async Task DeactiveUserAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user != null)
            {
                user.IsActive = false;
                await _userRepository.UpdateAsync(user);
            }
        }
    }
}
