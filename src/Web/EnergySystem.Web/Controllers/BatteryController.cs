namespace EnergySystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BatteryController : BaseController
    {
        // GET
        public IActionResult BatteryManagement() => this.View();
    }
}
