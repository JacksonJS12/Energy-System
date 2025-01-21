namespace EnergySystem.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Report;

    using ViewModels;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IReportService _reportService;
        public HomeController(IReportService reportService)
        {
            this._reportService = reportService;
        }
        public IActionResult Index() => this.View();

        public IActionResult Privacy() => this.View();

        public IActionResult Reports(DateTime? startDate, DateTime? endDate)
{
    // Default date range: Last 7 days
    if (!startDate.HasValue)
    {
        startDate = DateTime.Today.AddDays(-7);
    }

    if (!endDate.HasValue)
    {
        endDate = DateTime.Today;
    }

    var reports = this._reportService.GetReportsByDateRange(startDate.Value, endDate.Value);

    var model = new ReportViewModel
    {
        TotalUsage = reports.Sum(r => r.BatteryUsage),
        TotalSavings = reports.Sum(r => r.Savings),
        TotalCosts = reports.Sum(r => r.TotalCost),
        DetailedReports = reports,
    };

    ViewData["StartDate"] = startDate.Value.ToString("yyyy-MM-dd");
    ViewData["EndDate"] = endDate.Value.ToString("yyyy-MM-dd");

    return View(model);
}

        public IActionResult Alerts() => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View(
        new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
