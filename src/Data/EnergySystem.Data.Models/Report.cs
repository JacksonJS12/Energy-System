namespace EnergySystem.Data.Models
{
    using System;

    using Common.Models;

    using Microsoft.EntityFrameworkCore;

    public class Report : BaseDeletableModel<string>
    {
        public Report()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string PropertyId { get; set; }
        
        public string PropertyName { get; set; }
        
        public DateTime Date { get; set; } // The date for this report entry.
        [Precision(18, 2)]
        public decimal GridUsage { get; set; } // Total grid usage in kWh.
        [Precision(18, 2)]
        public decimal BatteryUsage { get; set; } // Total battery usage in kWh.
[Precision(18, 2)]
        public decimal GridCost { get; set; } // Total cost of grid electricity (BGN).
[Precision(18, 2)]
        public decimal BatteryCost { get; set; } // Total cost of battery electricity (BGN).
        [Precision(18, 2)]
        public decimal TotalCost { get; set; } // Combined cost.
        [Precision(18, 2)]
        public decimal Savings { get; set; } // Savings achieved by using battery power.
    }
}
