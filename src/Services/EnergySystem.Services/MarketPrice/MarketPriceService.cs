namespace EnergySystem.Services.Data.MarketPrice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;

    using Mapping;


    public class MarketPriceService : IMarketPriceService
    {
        private readonly IRepository<MarketPrice> _marketPriceRepository;
        private readonly IMapper _mapper;
        public MarketPriceService(IRepository<MarketPrice> marketPriceRepository, IMapper mapper)
        {
            this._marketPriceRepository = marketPriceRepository;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<T>> GetAll<T>(DateTime selectedDate)
        {
            var marketPrices = this._marketPriceRepository
                .AllAsNoTracking()
                .Where(mp => mp.Date.Date == selectedDate.Date)
                .OrderBy(x => x.Date)
                .ProjectTo<T>(this._mapper.ConfigurationProvider);
            return marketPrices;
        }
    }
}
