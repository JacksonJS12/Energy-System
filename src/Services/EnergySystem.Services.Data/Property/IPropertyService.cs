namespace EnergySystem.Services.Data.Property
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Projections;

    public interface IPropertyService
    {
        Task<PropertyProjection> GetPropertyDetailsAsync(string propertyId);

        Task<IEnumerable<PropertyProjection>> GetUserPropertiesAsync(string userId);

        Task<PropertyProjection> CreatePropertyAsync(PropertyProjection property);
    }
}
