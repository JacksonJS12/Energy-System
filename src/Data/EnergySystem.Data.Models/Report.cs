namespace EnergySystem.Data.Models
{
    using System;

    using EnergySystem.Data.Common.Models;

    public class Report : BaseDeletableModel<string>
    {
        public Report()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime Date { get; set; } // The date for this report entry.

        public double GridUsage { get; set; } // Total grid usage in kWh.

        public double BatteryUsage { get; set; } // Total battery usage in kWh.

        public decimal GridCost { get; set; } // Total cost of grid electricity (BGN).

        public decimal BatteryCost { get; set; } // Total cost of battery electricity (BGN).

        public decimal TotalCost { get; set; } // Combined cost.

        public decimal Savings { get; set; } // Savings achieved by using battery power.
    }
}    
