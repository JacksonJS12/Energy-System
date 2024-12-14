namespace EnergySystem.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using EnergySystem.Services.Data.Grid;
    using EnergySystem.Services.Data.Property;
    using EnergySystem.Web.ViewModels.ApplicationUser;
    using EnergySystem.Web.ViewModels.Property;
    using Microsoft.AspNetCore.Mvc;

    public class PropertyController : BaseController
    {
        private readonly IGridService _gridService;
        private readonly IMapper _mapper;
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService, IMapper mapper, IGridService gridService)
        {
            this._propertyService = propertyService;
            this._gridService = gridService;
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
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var ownerId = this.GetUserId();

            if (ownerId == null)
            {
                return this.Unauthorized();
            }

            var propertyProjections = await this._propertyService.GetUserPropertiesAsync(ownerId);

            var viewModel = new UserPropertiesViewModel
            {
                Properties = propertyProjections.Select(p =>
                    this._mapper.Map<PropertyDetailsViewModel>(p)).ToList(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var formModel = new PropertyFormModel
            {
                Grids = await this._gridService.GetAllGrids(),
            };

            return this.View(formModel);
        }
/*
        [HttpPost]
        public async Task<IActionResult> Create(PropertyFormModel model)
        {


            await this._propertyService.CreatePropertyAsync(model);
        }
        */
    }
}
