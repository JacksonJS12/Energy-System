namespace EnergySystem.Services.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IUserService
    {
        Task<string> GetFullNameByEmailAsync(string email);

        Task<string> GetFullNameByIdAsync(string userId);

        T GetById<T>(string userId);

        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
