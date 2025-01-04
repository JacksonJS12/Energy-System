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
    using Microsoft.EntityFrameworkCore;

    public class PropertyService : IPropertyService
    {
        private readonly IDeletableEntityRepository<Property> _propertyRepository;

        public PropertyService(IDeletableEntityRepository<Property> propertyRepository)
        {
            this._propertyRepository = propertyRepository;
        }

        public async Task<PropertyDetailsViewModel> GetPropertyDetailsAsync(string propertyId)
        {
            return await this._propertyRepository
                .All()
                .Where(p => p.Id == propertyId)
                .Select(p => new PropertyDetailsViewModel
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
                            MaximumCapacity = p.Grid.MaximumCapacity,
                            CurrentUsage = p.Grid.CurrentUsage,
                            SupplyPrice = p.Grid.SupplyPrice,
                            Provider = p.Grid.Provider,
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

        public async Task<IEnumerable<PropertyDetailsViewModel>> GetUserPropertiesAsync(string userId)
        {
            return await this._propertyRepository
                .All()
                .Where(p => p.OwnerId == userId)
                .Select(p => new PropertyDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    ElectricityNeed = p.ElectricityNeed,
                })
                .ToListAsync();
        }
        public Task CreateAsync(CreateInputModel property, string userId) => throw new NotImplementedException();
        // public Task CreateAsync(CreateInputModel input, string userId)
        // {
        //     var property = new Property
        //     {
        //         
        //     }
        // }
    }
}
