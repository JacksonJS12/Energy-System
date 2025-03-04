namespace EnergySystem.Services.Projections.Property
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Battery;

    using EnergySystem.Data.Models;
    using EnergySystem.Services.Mapping;

    using Grid;

    public class SinglePropertyProjection : IMapFrom<Property>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal ElectricityNeed { get; set; }
        
        public string OwnerId { get; set; }
        
        public GridProjection Grid { get; set; }

        public IEnumerable<BatteryProjection> Batteries { get; set; }
    }
}
