namespace EnergySystem.Services.Data.Battery
{
    using System.Threading.Tasks;

    using EnergySystem.Web.ViewModels.Battery;

    public interface IBatteryService
    {
        public Task CreateAsync(CreateBatteryInputModel model, string propertyId);
    }
}
