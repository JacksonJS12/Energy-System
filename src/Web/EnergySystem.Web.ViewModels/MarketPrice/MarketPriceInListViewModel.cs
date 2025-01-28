namespace EnergySystem.Web.ViewModels.MarketPrice
{
    using System;

    using Data.Models;

    using Services.Mapping;

    public class MarketPriceInListViewModel : IMapFrom<MarketPrice>
    {
        public string Hour { get; set; }
        public DateTime Date { get; set; }
        public decimal PricePerKWh { get; set; } // Price in BGN/kWh
    }
}
