namespace EnergySystem.Services.Background
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using EnergySystem.Services.Data.Report;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class DailyReportBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DailyReportBackgroundService> _logger;

        public DailyReportBackgroundService(IServiceProvider serviceProvider, ILogger<DailyReportBackgroundService> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("DailyReportBackgroundService is starting.");
            
            //for testing
            // using var scope = _serviceProvider.CreateScope();
            // var scraperService = scope.ServiceProvider.GetRequiredService<IMarketPricesWebScraperService>();
            // var reportService = scope.ServiceProvider.GetRequiredService<IReportService>();
            //
            // await scraperService.GetMarketPrices();
            // await reportService.GenerateDailyReportsAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                var nowUtc = DateTime.UtcNow; // Current time in UTC
                var eestTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"); // EEST timezone (UTC+2)
                var nowEest = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, eestTimeZone); // Convert UTC to EEST
            
                var nextRunEest = nowEest.Date.AddDays(nowEest.Hour >= 12 ? 1 : 0).AddHours(12); // Schedule at 12 PM EEST
                var delay = nextRunEest - nowEest; // Calculate delay in EEST
            
            
                _logger.LogInformation($"Next report generation scheduled for: {nextRunEest}");
            
                if (delay > TimeSpan.Zero)
                {
                    try
                    {
                        await Task.Delay(delay, stoppingToken);
                    }
                    catch (TaskCanceledException)
                    {
                        _logger.LogInformation("Task was canceled.");
                        break;
                    }
                }
            
                await GenerateDailyReportsAsync(stoppingToken);
            }

            _logger.LogInformation("DailyReportBackgroundService is stopping.");
        }

        private async Task GenerateDailyReportsAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            try
            {
                var scraperService = scope.ServiceProvider.GetRequiredService<IMarketPricesWebScraperService>();
                var reportService = scope.ServiceProvider.GetRequiredService<IReportService>();

                _logger.LogInformation("Daily report generation started.");

                // Fetch market prices for the day
                _logger.LogInformation("Fetching market prices...");
                await scraperService.GetMarketPrices();
                _logger.LogInformation("Market prices fetched successfully.");

                // Generate daily reports
                _logger.LogInformation("Generating daily reports...");
                await reportService.GenerateDailyReportsAsync();
                _logger.LogInformation("Daily reports generated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during daily report generation.");
            }
        }
    }
}
