namespace EnergySystem.Data.Models
{
    using System;
    using EnergySystem.Data.Common.Models;

    using Microsoft.EntityFrameworkCore;

    public class GridPriceEntry : BaseDeletableModel<int>
    {
        public string PropertyId { get; set; } // Foreign key to Property
        public Property Property { get; set; } // Navigation property

        public int Hour { get; set; } // Hour of the day (0–23)
        [Precision(18, 2)]
        public decimal ElectricityUsed { get; set; } // kWh used in this hour

        public string MarketPriceId { get; set; } // Foreign key to MarketPrice
        public MarketPrice MarketPrice { get; set; } // Navigation property
    }
}
