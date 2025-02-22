namespace EnergySystem.Services.Data.Battery
{
    using System.Threading.Tasks;

    using EnergySystem.Web.ViewModels.Battery;

    public interface IBatteryService
    {
        public Task CreateAsync(CreateBatteryInputModel model, string propertyId);
        public T GetById<T>(string batteryId, string userId);
        public Task UpdateAsync(EditBatteryInputModel input, string batteryId, string userId);
        public Task DeleteAsync(string batteryId, string userId);
    }
}
