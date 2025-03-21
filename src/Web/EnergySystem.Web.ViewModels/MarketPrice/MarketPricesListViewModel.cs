﻿namespace EnergySystem.Web.ViewModels.MarketPrice
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class MarketPricesListViewModel
    {
        IEnumerable<MarketPriceInListViewModel> MarketPrices { get; set; }

        public ICollection<decimal> Prices()
        {
            return this.MarketPrices.Select(x => x.PricePerKWh).ToList();
        }
    }
}
