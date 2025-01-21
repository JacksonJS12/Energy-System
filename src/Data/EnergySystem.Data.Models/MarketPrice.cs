﻿namespace EnergySystem.Data.Models
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

        public DateTime Hour { get; set; } // The specific hour of the price
        [Precision(18, 2)]
        public decimal PricePerKWh { get; set; } // Price in BGN/kWh

        public string Region { get; set; } // Optional: If prices differ by region or marke
    }
}
