namespace EnergySystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BatteryController : Controller
    {
        // GET
        public IActionResult BatteryManagement()
        {
            return View();
        }
    }
}
