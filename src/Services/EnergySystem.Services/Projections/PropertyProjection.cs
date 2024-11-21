namespace EnergySystem.Services.Projections
{
    using System.Collections.Generic;

    public class PropertyProjection
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public float ElectricityNeed { get; set; }

        public GridProjection Grid { get; set; }

        public IEnumerable<BatteryProjection> Batteries { get; set; }
    }
}
