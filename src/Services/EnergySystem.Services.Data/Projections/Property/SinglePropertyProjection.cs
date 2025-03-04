namespace EnergySystem.Services.Data.Projections.Property
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Battery;

    using EnergySystem.Data.Models;
    using EnergySystem.Services.Mapping;
    using EnergySystem.Web.ViewModels.Battery;
    using EnergySystem.Web.ViewModels.Grid;

    public class SinglePropertyProjection : IMapFrom<Property>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal ElectricityNeed { get; set; }
        
        public string OwnerId { get; set; }
        
        public GridViewModel Grid { get; set; }

        public IEnumerable<BatteryProjection> Batteries { get; set; }
    }
}
