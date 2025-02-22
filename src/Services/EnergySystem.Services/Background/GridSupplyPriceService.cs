namespace EnergySystem.Services.Background
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using EnergySystem.Data;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class GridSupplyPriceService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<GridSupplyPriceService> _logger;

        public GridSupplyPriceService(IServiceProvider serviceProvider, ILogger<GridSupplyPriceService> logger)
        {
            this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._logger.LogInformation("GridSupplyPriceService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Create a scope for the DbContext
                    using var scope = this._serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    // Perform the price update
                    await UpdateGridPricesAsync(dbContext, stoppingToken);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, "An error occurred while updating grid supply prices.");
                }

                // Wait for 1 hour before running again
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }

            this._logger.LogInformation("GridSupplyPriceService is stopping.");
        }

        private async Task UpdateGridPricesAsync(ApplicationDbContext dbContext, CancellationToken stoppingToken)
        {
            // Get current EEST time
            var nowUtc = DateTime.UtcNow;
            var eestTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            var nowEest = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, eestTimeZone);

            // Get the current hour and date in EEST
            var currentHour = nowEest.Hour;
            var currentDate = nowEest.Date.AddHours(nowEest.Hour);


            this._logger.LogInformation($"Current EEST time: {nowEest}. Fetching supply price for {currentDate:yyyy-MM-dd} hour {currentHour}.");
            
            // Query the MarketPrices table for the current hour and date
            var marketPrice = await dbContext.MarketPrices
                .Where(mp => mp.Date == currentDate)
                .OrderBy(mp => mp.Date)
                .Select(mp => mp.PricePerKWh)
                .FirstOrDefaultAsync(stoppingToken);

            
            if (marketPrice == 0)
            {
                this._logger.LogWarning("No market price found for the current hour. Skipping update.");
                return;
            }

            this._logger.LogInformation($"Market price found: {marketPrice:C} per kWh.");

            // Update all grids with the new supply price
            var grids = await dbContext.Grids.ToListAsync(stoppingToken);
            foreach (var grid in grids)
            {
                grid.SupplyPrice = marketPrice;
            }

            await dbContext.SaveChangesAsync(stoppingToken);
            this._logger.LogInformation("Supply prices updated successfully for all grids.");
        }
    }
}