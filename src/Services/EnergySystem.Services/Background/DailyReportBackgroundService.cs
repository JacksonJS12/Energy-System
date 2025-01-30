namespace EnergySystem.Services.Background
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using EnergySystem.Services.Data.Report;

    public class ReportGenerationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReportGenerationBackgroundService> _logger;

        public ReportGenerationBackgroundService(IServiceProvider serviceProvider, ILogger<ReportGenerationBackgroundService> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ReportGenerationBackgroundService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var nowUtc = DateTime.UtcNow;
                var eestTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
                var nowEest = TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(nowUtc, DateTimeKind.Utc), eestTimeZone);
                var nextRunEest = nowEest.Date.AddDays(nowEest.Hour >= 12 ? 1 : 0).AddHours(12);
                var delay = nextRunEest - nowEest;

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

                await GenerateReportsAsync(stoppingToken);
            }

            _logger.LogInformation("ReportGenerationBackgroundService is stopping.");
        }


        private async Task GenerateReportsAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            try
            {
                var reportService = scope.ServiceProvider.GetRequiredService<IReportService>();
                _logger.LogInformation("Generating daily reports...");
                await reportService.GenerateDailyReportsAsync();
                _logger.LogInformation("Daily reports generated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating daily reports.");
            }
        }
    }
}
