namespace EnergySystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PriceController : BaseController
    {
        // GET
        public IActionResult PriceTracker() => this.View();

        public IActionResult CostAnalysis() => this.View();
    }
}
