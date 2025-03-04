namespace EnergySystem.Services.Battery
{
    using EnergySystem.Services.Projections.Battery;
    using System.Threading.Tasks;


    public interface IBatteryService
    {
        public Task CreateAsync(CreateBatteryInputProjection model, string propertyId);
        public T GetById<T>(string batteryId, string userId);
        public Task UpdateAsync(EditBatteryInputProjection input, string batteryId, string userId);
        public Task DeleteAsync(string batteryId);
    }
}
