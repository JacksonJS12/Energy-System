namespace EnergySystem.Services.Data.MarketPrice
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMarketPriceService
    {
        public Task<IEnumerable<T>> GetAll<T>(DateTime selectedDate);
    }
}
