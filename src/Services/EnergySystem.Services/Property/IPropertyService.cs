namespace EnergySystem.Services.Property
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Projections.Property;

    public interface IPropertyService
    {
        public Task<SinglePropertyProjection> GetPropertyDetailsAsync(string propertyId, string userId);

        public T GetById<T>(string propertyId, string userId);

        public Task CreateAsync(CreateInputProjection property, string userId);

        public IEnumerable<T> GetAll<T>(string userId);
        public Task DeleteAsync(string propertyId, string userId);
        public Task UpdateAsync(EditPropertyInputProjection input, string propertyId, string userId);
        public T GetPowerPanelByPropertyId<T>(string propertyId, string userId);
    }
}
