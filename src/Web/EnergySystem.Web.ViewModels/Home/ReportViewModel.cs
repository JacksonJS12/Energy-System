namespace EnergySystem.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using Data.Models;
    using Services.Mapping;

    public class ReportViewModel : IMapFrom<Report>
    {
        public string Id { get; set; }
        public string PropertyId { get; set; }
        public Property Property { get; set; }
        public double BatteryUsage { get; set; }
        public decimal GridCost { get; set; }
        public DateTime Date { get; set; }
        public decimal BatteryCost { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Savings { get; set; }

        // Add aggregated data
        public decimal TotalUsage { get; set; }
        public decimal TotalSavings { get; set; }
        public decimal TotalCosts { get; set; }

        // Filtered reports for week, month, or year
        public IEnumerable<Report> DetailedReports { get; set; }
    }
}
