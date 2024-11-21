namespace EnergySystem.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using EnergySystem.Services.Data.Property;
    using EnergySystem.Web.ViewModels.Battery;
    using EnergySystem.Web.ViewModels.Grid;
    using EnergySystem.Web.ViewModels.Property;

    using Microsoft.AspNetCore.Mvc;

    using ViewModels.ApplicationUser;

    public class PropertyController : BaseController
    {
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public PropertyController(IPropertyService propertyService, IMapper mapper)
        {
            this._propertyService = propertyService;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Details(string id)
        {
            var propertyProjection = await this._propertyService.GetPropertyDetailsAsync(id);

            if (propertyProjection == null)
            {
                return this.NotFound();
            }

            var viewModel = this._mapper.Map<PropertyDetailsViewModel>(propertyProjection);
            return this.View(viewModel);
        }
        
        public async Task<IActionResult> MyProperties()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var ownerId = GetUserId();

            if (ownerId == null)
            {
                return Unauthorized();
            }

            var propertyProjections = await this._propertyService.GetUserPropertiesAsync(ownerId);

            var viewModel = new UserPropertiesViewModel
            {
                Properties = propertyProjections.Select(p =>
                    this._mapper.Map<PropertyDetailsViewModel>(p)).ToList(),
            };

            return this.View(viewModel);
        }
    }
}
