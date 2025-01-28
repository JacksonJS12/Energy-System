namespace EnergySystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PriceController : BaseController
    {
        // GET
        public IActionResult PriceTracker()
        {
            return this.View();
        }

        public IActionResult CostAnalysis() => this.View();
    }
}
