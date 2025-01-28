namespace EnergySystem.Data.Models
{
    using System;

    using Common.Models;

    using Microsoft.EntityFrameworkCore;

    public class MarketPrice : BaseDeletableModel<string>
    {
        public MarketPrice()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime Date { get; set; } 
        public string Hour { get; set; }
        [Precision(18, 2)]
        public decimal PricePerKWh { get; set; } // Price in BGN/kWh

    }
}
