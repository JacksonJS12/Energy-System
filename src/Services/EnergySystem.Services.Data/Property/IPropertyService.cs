namespace EnergySystem.Services.Data.Property
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EnergySystem.Web.ViewModels.Property;

    public interface IPropertyService
    {
        public Task<SinglePropertyViewModel> GetPropertyDetailsAsync(string propertyId);

        public T GetById<T>(string propertyId);

        public Task CreateAsync(CreateInputModel property, string userId);

        public IEnumerable<T> GeAll<T>(string userId);
        public Task DeleteAsync(string propertyId);
    }
}
