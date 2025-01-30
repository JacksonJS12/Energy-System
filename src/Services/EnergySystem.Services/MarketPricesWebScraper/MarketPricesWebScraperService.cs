namespace EnergySystem.Services.Background
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AngleSharp;
    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;

    public class MarketPricesWebScraperService : IMarketPricesWebScraperService
    {
        private readonly IRepository<MarketPrice> _marketPriceRepository;

        public MarketPricesWebScraperService(IRepository<MarketPrice> marketPriceRepository)
        {
            this._marketPriceRepository = marketPriceRepository;
        }
        
        public async Task GetMarketPricesForDayAhead()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://ibex.bg/dam-data-chart-2/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            // Scrape price, hour, and date information
            var priceList = document.QuerySelectorAll("td.column-price")
                .Select(x => Decimal.Parse(x.InnerHtml))
                .ToArray();
            var hourList = document.QuerySelectorAll("td.column-time_part")
                .Select(x => TimeSpan.Parse(x.InnerHtml)) // Parse as TimeSpan (e.g., "00:00:00")
                .ToArray();
            var dateList = document.QuerySelectorAll("td.column-date_part")
                .Select(x => DateTime.Parse(x.InnerHtml)) // Parse as DateTime
                .ToArray();

            var eestTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"); // EEST (UTC+2)

            // Iterate over the data and store it
            for (int i = 0; i < 24; i++)
            {
                // Convert date and time from CET to EEST
                var cetDateTime = dateList[i].Add(hourList[i]); // Combine date and hour as CET
                var eestDateTime = TimeZoneInfo.ConvertTime(cetDateTime, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"), eestTimeZone);

                // Create MarketPrice entity
                var marketPrice = new MarketPrice
                {
                    Hour = eestDateTime.Hour.ToString(),        // Save time in EEST
                    PricePerKWh = priceList[i] / 1000, // from MWh to kWh
                    Date = eestDateTime   // Store the date component in EEST
                };

                await this._marketPriceRepository.AddAsync(marketPrice);
            }

            await this._marketPriceRepository.SaveChangesAsync();
        }
    }
}
