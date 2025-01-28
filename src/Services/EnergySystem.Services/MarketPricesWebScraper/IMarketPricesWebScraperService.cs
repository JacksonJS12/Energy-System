namespace EnergySystem.Services.Background
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AngleSharp;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;

    using Microsoft.Extensions.Hosting;

    public interface IMarketPricesWebScraperService
    {
        public Task GetMarketPrices();
    }
}
