namespace EnergySystem.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class DashboardController : AdministrationController
    {

        public DashboardController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
