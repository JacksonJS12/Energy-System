namespace EnergySystem.Services.Data.Property
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EnergySystem.Web.ViewModels.Property;

    public interface IPropertyService
    {
        Task<PropertyDetailsViewModel> GetPropertyDetailsAsync(string propertyId);

        public T GetById<T>(string userId);

        Task CreateAsync(CreateInputModel property, string userId);
        IEnumerable<T> GeAll<T>(string userId);
    }
}
