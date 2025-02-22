namespace EnergySystem.Services.Data.Property
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EnergySystem.Web.ViewModels.Property;

    public interface IPropertyService
    {
        public Task<SinglePropertyViewModel> GetPropertyDetailsAsync(string propertyId, string userId);

        public T GetById<T>(string propertyId, string userId);

        public Task CreateAsync(CreateInputModel property, string userId);

        public IEnumerable<T> GeAll<T>(string userId);
        public Task DeleteAsync(string propertyId, string userId);
        public Task UpdateAsync(EditPropertyInputModel input, string propertyId, string userId);
    }
}
