namespace EnergySystem.Services.Data.Report
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;

    public interface IReportService
    {
        public Task GenerateDailyReportsAsync();
        public IEnumerable<Report> GetReportsByDateRange(DateTime startDate, DateTime endDate);
    }
}
