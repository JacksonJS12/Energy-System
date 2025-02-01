using System;
using System.Threading;
using System.Threading.Tasks;

using EnergySystem.Services.Background;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class MarketPricesScraperBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MarketPricesScraperBackgroundService> _logger;

    public MarketPricesScraperBackgroundService(IServiceProvider serviceProvider, ILogger<MarketPricesScraperBackgroundService> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this._logger.LogInformation("MarketPricesScraperBackgroundService is starting.");
        // hard test
       // await ScrapeMarketPricesAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            var eestTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            var nowUtc = DateTime.UtcNow;
            var nowEest = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, eestTimeZone);

            // Schedule next execution for 14:30 EEST
            // Change to test if the Background Service works correct
            var nextRunEest = nowEest.Date.AddHours(14).AddMinutes(30);
            if (nowEest >= nextRunEest)
            {
                // If it's already past 14:30 today, schedule for tomorrow
                nextRunEest = nextRunEest.AddDays(1);
            }

            var delay = nextRunEest - nowEest;

            this._logger.LogInformation($"Next market price scraping scheduled for: {nextRunEest} EEST");

            if (delay > TimeSpan.Zero)
            {
                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    this._logger.LogInformation("Task was canceled.");
                    break;
                }
            }

            await ScrapeMarketPricesAsync(stoppingToken);
        }

        this._logger.LogInformation("MarketPricesScraperBackgroundService is stopping.");
    }

    private async Task ScrapeMarketPricesAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();

        try
        {
            var scraperService = scope.ServiceProvider.GetRequiredService<IMarketPricesWebScraperService>();

            this._logger.LogInformation("Fetching latest market prices...");
            await scraperService.GetMarketPricesForDayAhead();

            this._logger.LogInformation("Market prices updated successfully.");
        }
        catch (Exception ex)
        {
            this._logger.LogError(ex, "An error occurred while fetching market prices.");
        }
    }
}
