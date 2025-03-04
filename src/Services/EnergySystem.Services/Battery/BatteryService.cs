namespace EnergySystem.Services.Battery
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Services.Mapping;

    using Projections.Battery;

    using Battery=global::Battery;

    public class BatteryService : IBatteryService
    {
        private readonly IDeletableEntityRepository<Battery> _batteryRepository;
        private readonly IMapper _mapper;

        public BatteryService(IDeletableEntityRepository<Battery> batteryRepository, IMapper mapper)
        {
            this._batteryRepository = batteryRepository;
            this._mapper = mapper;
        }

        public async Task CreateAsync(CreateBatteryInputProjection model, string propertyId)
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
        public T GetById<T>(string batteryId, string userId)
        {
            var battery = this._batteryRepository.AllAsNoTracking()
                .Where(x => x.Id == batteryId && x.Property.OwnerId == userId)
                .ProjectTo<T>(this._mapper.ConfigurationProvider)
                .FirstOrDefault();

            return battery;
        }
        public async Task UpdateAsync(EditBatteryInputProjection input, string batteryId, string userId)
        {
            var battery = this._batteryRepository.All().FirstOrDefault(x => x.Id == batteryId && x.Property.OwnerId == userId);
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
            var battery = this._batteryRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == batteryId);
            this._batteryRepository.Delete(battery);
            await this._batteryRepository.SaveChangesAsync();
        }
    }

}
