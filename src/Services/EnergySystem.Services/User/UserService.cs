namespace EnergySystem.Services.User
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;

    using Mapping;

    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> _userRepository;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            var user = await this._userRepository
                .AllAsNoTracking()
                .Where(u => u.Email == email)
                .Select(u => new { u.FirstName, u.LastName })
                .FirstOrDefaultAsync();

            return user != null ? $"{user.FirstName} {user.LastName}" : string.Empty;
        }

        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            var user = await this._userRepository
                .AllAsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new { u.FirstName, u.LastName })
                .FirstOrDefaultAsync();

            return user != null ? $"{user.FirstName} {user.LastName}" : string.Empty;
        }

        public T GetById<T>(string userId)
        {
            return this._userRepository
                .AllAsNoTracking()
                .Where(u => u.Id == userId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this._userRepository
                .AllAsNoTracking()
                .OrderBy(u => u.FirstName) // Example: Order users by first name
                .To<T>()
                .ToListAsync();
        }
    }
}
