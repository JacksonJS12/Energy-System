namespace EnergySystem.Services.Data.Property
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;
    using EnergySystem.Web.ViewModels.Battery;
    using EnergySystem.Web.ViewModels.Grid;
    using EnergySystem.Web.ViewModels.Property;

    using Mapping;

    using Microsoft.EntityFrameworkCore;

    public class PropertyService : IPropertyService
    {
        private readonly IDeletableEntityRepository<Property> _propertyRepository;

        public PropertyService(IDeletableEntityRepository<Property> propertyRepository)
        {
            this._propertyRepository = propertyRepository;
        }

        public async Task<SinglePropertyViewModel> GetPropertyDetailsAsync(string propertyId)
        {
            return await this._propertyRepository
                .All()
                .Where(p => p.Id == propertyId)
                .Select(p => new SinglePropertyViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    ElectricityNeed = p.ElectricityNeed,
                    Grid = p.Grid == null
                        ? null
                        : new GridViewModel()
                        {
                            Id = p.Grid.Id,
                            Name = p.Grid.Name,
                            CurrentUsage = p.Grid.CurrentUsage,
                            SupplyPrice = p.Grid.SupplyPrice,
                        },
                    Batteries = p.Batteries.Select(b => new BatteryViewModel
                    {
                        Id = b.Id,
                        Model = b.Model,
                        Capacity = b.Capacity,
                        Voltage = b.Voltage,
                        CurrentChargeLevel = b.CurrentChargeLevel,
                        StateOfHealth = b.StateOfHealth,
                        CycleCount = b.CycleCount,
                    }).ToList(),
                })
                .FirstOrDefaultAsync();
        }

        public T GetById<T>(string propertyId)
        {
            var property = this._propertyRepository.AllAsNoTracking()
                .Where(x => x.Id == propertyId)
                .To<T>().FirstOrDefault();

            return property;
        }
        public async Task CreateAsync(CreateInputModel input, string userId)
        {
            var property = new Property
            {
                Name = input.Name,
                Address = input.Address,
                ElectricityNeed =input.ElectricityNeed,
                GridId = input.GridId,
                OwnerId = userId,
            };

            await this._propertyRepository.AddAsync(property);
            await this._propertyRepository.SaveChangesAsync();
        }
        public IEnumerable<T> GeAll<T>(string userId)
        {
            var properties = this._propertyRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Where(x => x.OwnerId == userId)
                .To<T>()
                .ToList();
            return properties;
        }
        public async Task DeleteAsync(string propertyId)
        {
            var property = this._propertyRepository.All().FirstOrDefault(x => x.Id == propertyId);
            this._propertyRepository.Delete(property);
            await this._propertyRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(EditPropertyInputModel input, string propertyId)
        {
            var property = this._propertyRepository.All().FirstOrDefault(x => x.Id == propertyId);
            property.Name = input.Name;
            property.Address = input.Address;
            property.ElectricityNeed = input.ElectricityNeed;
            property.GridId = input.GridId;
            await this._propertyRepository.SaveChangesAsync();
        }
    }
}
