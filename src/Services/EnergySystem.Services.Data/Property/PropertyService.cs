namespace EnergySystem.Services.Data.Property
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;

    using Microsoft.EntityFrameworkCore;

    using Projections;

    public class PropertyService : IPropertyService
    {
        private readonly IDeletableEntityRepository<Property> _propertyRepository;

        public PropertyService(IDeletableEntityRepository<Property> propertyRepository)
        {
            this._propertyRepository = propertyRepository;
        }

        public async Task<PropertyProjection> GetPropertyDetailsAsync(string propertyId)
        {
            return await this._propertyRepository
                .All()
                .Where(p => p.Id == propertyId)
                .Select(p => new PropertyProjection
                {
                    Id = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    ElectricityNeed = p.ElectricityNeed,
                    Grid = p.Grid == null
                        ? null
                        : new GridProjection
                        {
                            Id = p.Grid.Id,
                            Name = p.Grid.Name,
                            MaximumCapacity = p.Grid.MaximumCapacity,
                            CurrentUsage = p.Grid.CurrentUsage,
                            SupplyPrice = p.Grid.SupplyPrice,
                            Provider = p.Grid.Provider,
                        },
                    Batteries = p.Batteries.Select(b => new BatteryProjection
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

        public async Task<IEnumerable<PropertyProjection>> GetUserPropertiesAsync(string userId)
        {
            return await this._propertyRepository
                .All()
                .Where(p => p.OwnerId == userId)
                .Select(p => new PropertyProjection
                {
                    Id = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    ElectricityNeed = p.ElectricityNeed,
                })
                .ToListAsync();
        }
        public Task<PropertyProjection> CreatePropertyAsync(PropertyProjection property) => throw new NotImplementedException();
    }
}
