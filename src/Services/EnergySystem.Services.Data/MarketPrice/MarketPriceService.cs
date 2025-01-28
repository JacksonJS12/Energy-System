namespace EnergySystem.Services.Data.MarketPrice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;

    using Mapping;

    public class MarketPriceService : IMarketPriceService
    {
        private readonly IRepository<MarketPrice> _marketPriceRepository;
        public MarketPriceService(IRepository<MarketPrice> marketPriceRepository)
        {
            this._marketPriceRepository = marketPriceRepository;
        }
        public async Task<IEnumerable<T>> GeAll<T>(DateTime selectedDate)
        {
            var marketPrices = this._marketPriceRepository
                .AllAsNoTracking()
                .Where(mp => mp.Date.Date == selectedDate.Date)
                .OrderBy(x => x.Date)
                .To<T>()
                .ToList();
            return marketPrices;
        }
    }
}
