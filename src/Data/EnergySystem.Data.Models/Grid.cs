namespace EnergySystem.Data.Models
{
    using System;

    using Common.Models;

    using Microsoft.EntityFrameworkCore;

    public class Grid : BaseDeletableModel<string>
    {
        public Grid()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        [Precision(18, 2)]
        public decimal CurrentUsage { get; set; } // Real-time energy consumption (kWh)
        [Precision(18, 2)]
        public decimal SupplyPrice { get; set; } // Cost per kWh of electricity

        public string MarketPriceId { get; set; }
        
        public virtual MarketPrice MarketPrice { get; set; }
    }
}
