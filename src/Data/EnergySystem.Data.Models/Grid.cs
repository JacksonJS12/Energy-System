namespace EnergySystem.Data.Models
{
    using System;

    using Common.Models;

    public class Grid : BaseDeletableModel<string>
    {
        public Grid()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public float MaximumCapacity { get; set; } // Maximum energy supply capacity (kWh)

        public float CurrentUsage { get; set; } // Real-time energy consumption (kWh)

        public float SupplyPrice { get; set; } // Cost per kWh of electricity

        public string Provider { get; set; } // Name of the energy provider

        public DateTime CreatedOn { get; set; }
    }
}
