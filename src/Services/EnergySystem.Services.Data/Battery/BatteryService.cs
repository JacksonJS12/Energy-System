namespace EnergySystem.Services.Data.Battery
{
    using System;
    using System.Threading.Tasks;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;

    using EnergySystem.Web.ViewModels.Battery;

    public class BatteryService : IBatteryService
    {
        private readonly IDeletableEntityRepository<Battery> _batteryRepository;

        public BatteryService(IDeletableEntityRepository<Battery> batteryRepository)
        {
            this._batteryRepository = batteryRepository;
        }

        public async Task CreateAsync(CreateBatteryInputModel model, string propertyId)
        {
            var battery = new Battery
            {
                Model = model.Model,
                Capacity = model.Capacity,
                Voltage = model.Voltage,
                Manufacturer = model.Manufacturer,
                ManufactureDate = model.ManufactureDate,
                InitialInstallation = model.InitialInstallation,
                CurrentChargeLevel = model.CurrentChargeLevel,
                StateOfHealth = model.StateOfHealth,
                CycleCount = model.CycleCount,
                Temperature = model.Temperature,
                LifetimeEnergyStored = model.LifetimeEnergyStored,
                PropertyId = propertyId, // Associate the battery with the property
            };

            await this._batteryRepository.AddAsync(battery);
            await this._batteryRepository.SaveChangesAsync();
        }
    }

}
