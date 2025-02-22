namespace EnergySystem.Web.ViewModels.Property
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EnergySystem.Data.Models;
    using EnergySystem.Services.Mapping;
    using EnergySystem.Web.ViewModels.Battery;
    using EnergySystem.Web.ViewModels.Grid;

    public class SinglePropertyViewModel : IMapFrom<Property>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal ElectricityNeed { get; set; }
        
        public string OwnerId { get; set; }
        
        public GridViewModel Grid { get; set; }

        public IEnumerable<BatteryViewModel> Batteries { get; set; }
    }
}
