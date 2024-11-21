namespace EnergySystem.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        protected string GetUserId()
        {
            string id = string.Empty;

            if (User != null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return id;
        }
    }
}
