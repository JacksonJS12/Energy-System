namespace EnergySystem.Data.Models
{
    using System;

    using EnergySystem.Data.Common.Models;

    public class MarketPrice : BaseDeletableModel<string>
    {
        public MarketPrice()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime Hour { get; set; } // The specific hour of the price

        public decimal PricePerKWh { get; set; } // Price in BGN/kWh

        public string Region { get; set; } // Optional: If prices differ by region or marke
    }
}
