namespace EnergySystem.Services.Background.GridPriceEntry
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;

    using Microsoft.EntityFrameworkCore;

    public class GridPriceEntryService : IGridPriceEntryService
    {
        private readonly IRepository<GridPriceEntry> _gridPriceEntryRepository;
        private readonly IRepository<MarketPrice> _marketPriceRepository;

        public GridPriceEntryService(IRepository<GridPriceEntry> gridPriceEntryRepository,IRepository<MarketPrice> marketPriceRepository)
        {
            this._gridPriceEntryRepository = gridPriceEntryRepository;
            this._marketPriceRepository = marketPriceRepository;
        }

        public async Task AddHourlyEntryAsync(string propertyId, int hour, decimal electricityUsed, string region = null)
        {
            var currentHour = DateTime.UtcNow.Date.AddHours(hour);

            // Get the corresponding market price
            var marketPrice = await this._marketPriceRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(p => p.Hour == currentHour && p.Region == region);

            if (marketPrice == null)
            {
                throw new InvalidOperationException($"Market price not found for hour {currentHour} and region {region ?? "default"}.");
            }

            // Check if an entry already exists for the hour
            var existingEntry = await this._gridPriceEntryRepository
                .All()
                .FirstOrDefaultAsync(e => e.PropertyId == propertyId && e.Hour == hour && e.CreatedOn.Date == DateTime.UtcNow.Date);

            if (existingEntry != null)
            {
                // Update the existing entry
                existingEntry.ElectricityUsed += electricityUsed;
                existingEntry.MarketPriceId = marketPrice.Id; // Update price association if needed
            }
            else
            {
                // Add a new entry
                var entry = new GridPriceEntry
                {
                    PropertyId = propertyId,
                    Hour = hour,
                    ElectricityUsed = electricityUsed,
                    MarketPriceId = marketPrice.Id,
                };

                await this._gridPriceEntryRepository.AddAsync(entry);
            }

            await this._gridPriceEntryRepository.SaveChangesAsync();
        }

        public async Task<decimal> CalculateDailyCostAsync(string propertyId)
        {
            var today = DateTime.UtcNow.Date;

            // Calculate total cost by summing electricity used * price per kWh
            var dailyCost = await this._gridPriceEntryRepository
                .AllAsNoTracking()
                .Where(e => e.PropertyId == propertyId && e.CreatedOn.Date == today)
                .SumAsync(e => e.ElectricityUsed * e.MarketPrice.PricePerKWh);

            return dailyCost;
        }
        
        public async Task ClearDailyEntriesAsync(string propertyId)
        {
            var today = DateTime.UtcNow.Date;

            // Query all entries for the given property and today's date
            var dailyEntries = this._gridPriceEntryRepository
                .All()
                .Where(e => e.PropertyId == propertyId && e.CreatedOn.Date == today);

            // Use DeleteRange for bulk deletion
            this._gridPriceEntryRepository.DeleteRange(dailyEntries);

            // Save changes
            await this._gridPriceEntryRepository.SaveChangesAsync();
        }


    }
}
