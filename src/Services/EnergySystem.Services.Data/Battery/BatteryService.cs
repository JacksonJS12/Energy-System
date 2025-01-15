namespace EnergySystem.Services.Data.Battery
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;

    using EnergySystem.Web.ViewModels.Battery;

    using Mapping;

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
        public T GetById<T>(string batteryId)
        {
            var battery = this._batteryRepository.AllAsNoTracking()
                .Where(x => x.Id == batteryId)
                .To<T>().FirstOrDefault();

            return battery;
        }
        public async Task UpdateAsync(EditBatteryInputModel input, string batteryId)
        {
            var battery = this._batteryRepository.All().FirstOrDefault(x => x.Id == batteryId);
            battery.Model = input.Model;
            battery.Capacity = input.Capacity;
            battery.Voltage = input.Voltage;
            battery.Manufacturer = input.Manufacturer;
            battery.ManufactureDate = input.ManufactureDate;
            battery.CurrentChargeLevel = input.CurrentChargeLevel;
            battery.StateOfHealth = input.StateOfHealth;
            battery.CycleCount = input.CycleCount;
            battery.Temperature = input.Temperature;
            battery.LifetimeEnergyStored = input.LifetimeEnergyStored;
            
            await this._batteryRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(string batteryId)
        {
            var battery = this._batteryRepository.All().FirstOrDefault(x => x.Id == batteryId);
            this._batteryRepository.Delete(battery);
            await this._batteryRepository.SaveChangesAsync();
        }
    }

}
