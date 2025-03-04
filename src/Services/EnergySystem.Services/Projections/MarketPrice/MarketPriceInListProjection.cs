namespace EnergySystem.Services.Projections.MarketPrice
{
    using System;

    using EnergySystem.Data.Models;

    using Services.Mapping;

    public class MarketPriceInListProjection : IMapFrom<MarketPrice>
    {
        public string Hour { get; set; }
        public DateTime Date { get; set; }
        public decimal PricePerKWh { get; set; } // Price in BGN/kWh
    }
}
