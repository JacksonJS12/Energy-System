namespace EnergySystem.Web.ViewModels.Property
{
    using System.Collections.Generic;

    using Battery;

    using Services.Projections;

    public class PropertyFormModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public float ElectricityNeed { get; set; }

        public IEnumerable<GridProjection> Grids { get; set; }

        public IEnumerable<BatteryViewModel> Batteries { get; set; } = new HashSet<BatteryViewModel>();
    }
}
