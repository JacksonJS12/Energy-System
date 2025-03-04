namespace EnergySystem.Services.Projections.MarketPrice
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class MarketPricesListProjection
    {
        IEnumerable<MarketPriceInListProjection> MarketPrices { get; set; }

        public ICollection<decimal> Prices()
        {
            return this.MarketPrices.Select(x => x.PricePerKWh).ToList();
        }
    }
}
