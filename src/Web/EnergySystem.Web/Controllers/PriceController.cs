namespace EnergySystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PriceController : BaseController
    {
        // GET
        public IActionResult PriceTracker()
        {
            return View();
        }
        
        public IActionResult CostAnalysis()
        {
            return View();
        }
    }
}
