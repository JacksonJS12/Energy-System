namespace EnergySystem.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EnergySystem.Web.ViewModels.User;

    public interface IUserService
    {
        Task<string> GetFullNameByEmailAsync(string email);

        Task<string> GetFullNameByIdAsync(string userId);

        T GetById<T>(string userId);

        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
