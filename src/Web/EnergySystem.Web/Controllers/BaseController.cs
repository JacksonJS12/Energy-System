namespace EnergySystem.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        protected string GetUserId()
        {
            var id = string.Empty;

            if (this.User != null)
            {
                id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return id;
        }
    }
}
