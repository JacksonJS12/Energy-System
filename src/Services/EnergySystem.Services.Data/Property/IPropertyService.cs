namespace EnergySystem.Services.Data.Property
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EnergySystem.Web.ViewModels.Property;

    public interface IPropertyService
    {
        Task<PropertyDetailsViewModel> GetPropertyDetailsAsync(string propertyId);

        Task<IEnumerable<PropertyDetailsViewModel>> GetUserPropertiesAsync(string userId);

        Task CreateAsync(CreateInputModel property, string userId);
    }
}
