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
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    // protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    // {
    //     _logger.LogInformation("DailyReportBackgroundService is starting.");
    //
    //     while (!stoppingToken.IsCancellationRequested)
    //     {
    //         try
    //         {
    //             var now = DateTime.Now;
    //
    //             // Calculate next 12 PM
    //             var nextRun = now.Date.AddDays(now.Hour >= 12 ? 1 : 0).AddHours(12);
    //             var delay = nextRun - now;
    //
    //             _logger.LogInformation($"Next report generation scheduled for: {nextRun}");
    //
    //             // Wait until the next run (12 PM)
    //             if (delay > TimeSpan.Zero)
    //             {
    //                 await Task.Delay(delay, stoppingToken);
    //             }
    //
    //             // Run the report generation task
    //             await GenerateDailyReportsAsync(stoppingToken);
    //         }
    //         catch (Exception ex)
    //         {
    //             _logger.LogError(ex, "An error occurred in DailyReportBackgroundService.");
    //         }
    //     }
    //
    //     _logger.LogInformation("DailyReportBackgroundService is stopping.");
    // }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DailyReportBackgroundService is starting for testing.");

        // Run the report generation task immediately for testing
        await GenerateDailyReportsAsync(stoppingToken);

        // Continue with normal scheduling
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRun = now.Date.AddDays(now.Hour >= 12 ? 1 : 0).AddHours(12);
            var delay = nextRun - now;

            _logger.LogInformation($"Next report generation scheduled for: {nextRun}");

            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay, stoppingToken);
            }

            await GenerateDailyReportsAsync(stoppingToken);
        }
    }

    private async Task GenerateDailyReportsAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var reportService = scope.ServiceProvider.GetRequiredService<IReportService>();

        try
        {
            _logger.LogInformation("Daily report generation started.");

            await reportService.GenerateDailyReportsAsync();

            _logger.LogInformation("Daily report generation completed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while generating reports.");
        }
    }
}
