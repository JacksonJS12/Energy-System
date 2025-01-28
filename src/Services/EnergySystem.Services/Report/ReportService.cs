namespace EnergySystem.Services.Report;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Background.GridPriceEntry;

using Data.Report;

using EnergySystem.Data.Common.Repositories;
using EnergySystem.Data.Models;

using GridPriceEntry;

using Microsoft.EntityFrameworkCore;

public class ReportService : IReportService
{
    private readonly IDeletableEntityRepository<Property> _propertyRepository;
    private readonly IRepository<Report> _reportRepository;
    private readonly IGridPriceEntryService _gridPriceEntryService;

    public ReportService(
        IDeletableEntityRepository<Property> propertyRepository,
        IRepository<Report> reportRepository, IGridPriceEntryService gridPriceEntryService)
    {
        this._propertyRepository = propertyRepository;
        this._reportRepository = reportRepository;
        this._gridPriceEntryService = gridPriceEntryService;
    }

    public async Task GenerateDailyReportsAsync()
    {
        var properties = this._propertyRepository
            .All()
            .Include(p => p.Batteries)
            .ToList();

        foreach (var property in properties)
        {
            // Calculate daily grid cost
            var dailyGridCost = await this._gridPriceEntryService.CalculateDailyCostAsync(property.Id);

            // Get total battery usage for the day
            decimal batteryUsage = property.Batteries.Sum(battery => battery.LifetimeEnergyStored - battery.LifetimeEnergyStoredAtStartOfDay);

            // Calculate battery cost
            decimal batteryCost = property.Batteries.Sum(battery =>
            {
                var usage = battery.LifetimeEnergyStored - battery.LifetimeEnergyStoredAtStartOfDay;
                return (decimal)usage * battery.PriceChargingAt;
            });

            // Calculate savings
            decimal savings = dailyGridCost - batteryCost;

            // Create and save the report
            var report = new Report
            {
                PropertyName = property.Name,
                PropertyId = property.Id,
                Date = DateTime.UtcNow.Date,
                GridUsage = property.GridPriceEntries.Sum(e => e.ElectricityUsed),
                BatteryUsage = batteryUsage,
                GridCost = dailyGridCost,
                BatteryCost = batteryCost,
                TotalCost = dailyGridCost + batteryCost,
                Savings = savings,
            };

            await this._reportRepository.AddAsync(report);

            // Clear daily grid price entries for the next day
            await this._gridPriceEntryService.ClearDailyEntriesAsync(property.Id);


            foreach (var battery in property.Batteries)
            {
                battery.LifetimeEnergyStoredAtStartOfDay = battery.LifetimeEnergyStored;
            }
        }

        await this._reportRepository.SaveChangesAsync();
    }

    private void ResetDailyUsage(Property property)
    {
        property.EnergyUsedFromGridToday = 0;

        foreach (var battery in property.Batteries)
        {
            battery.LifetimeEnergyStoredAtStartOfDay = battery.LifetimeEnergyStored;
        }
    }
    
    public IEnumerable<Report> GetReportsByDateRange(DateTime startDate, DateTime endDate)
    {
        return this._reportRepository
            .AllAsNoTracking()
            .Where(r => r.Date >= startDate && r.Date <= endDate)
            .ToList();
    }
}