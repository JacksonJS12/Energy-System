namespace EnergySystem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using EnergySystem.Web.ViewModels.Property;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Grid;
    using Services.Projections.Property;
    using Services.Property;

    public class PropertyController : BaseController
    {
        private readonly IGridService _gridService;
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public PropertyController(IPropertyService propertyService, IGridService gridService, IMapper mapper)
        {
            this._propertyService = propertyService;
            this._gridService = gridService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var formModel = new CreateInputModel();
            //{
            //Grids = this._gridService.GetAllGrids(),
            //};
            formModel.GridsItems = this._gridService.GetAllAsKeyValuePairs();
            return this.View(formModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateInputModel input)
        {
            var userId = this.GetUserId();

            CreateInputProjection projection = this._mapper.Map<CreateInputProjection>(input);

            await this._propertyService.CreateAsync(projection, userId);

            return this.RedirectToAction("All", "Property");
        }

        [HttpGet]
        [Authorize]
        public IActionResult All()
        {
            string userId = this.GetUserId();

            var projection = new PropertiesListProjection
            {
                Properties = this._propertyService.GetAll<PropertyInListProjection>(userId),
            };

            PropertiesListViewModel viewModel = this._mapper.Map<PropertiesListViewModel>(projection);

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(string id)
        {
            string userId = this.GetUserId();

            var projection = this._propertyService.GetById<SinglePropertyProjection>(id, userId);
            if (projection == null)
            {
                return this.Forbid();
            }

            var property = this._mapper.Map<SinglePropertyViewModel>(projection);

            return this.View(property);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            string userId = this.GetUserId();

            try
            {
                await this._propertyService.DeleteAsync(id, userId);
            }
            catch (Exception)
            {
                return this.Forbid();
            }

            return this.RedirectToAction("All", "Property");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(string id)
        {
            string userId = this.GetUserId();

            var projection = this._propertyService.GetById<EditPropertyInputProjection>(id, userId);

            if (projection == null)
            {
                return this.Forbid();
            }

            projection.GridsItems = this._gridService.GetAllAsKeyValuePairs();

            var ViewModel = this._mapper.Map<EditPropertyInputModel>(projection);

            return this.View(ViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditPropertyInputModel input, string id)
        {
            string userId = this.GetUserId();

            if (!this.ModelState.IsValid)
            {
                input.GridsItems = this._gridService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var projection = this._mapper.Map<EditPropertyInputProjection>(input);

            try
            {
                await this._propertyService.UpdateAsync(projection, id, userId);
            }
            catch (Exception)
            {
                return this.Forbid();
            }

            return this.RedirectToAction("Details", "Property", new { id });
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult PowerPanel(string id)
        {
            string userId = this.GetUserId();

            var projection = this._propertyService.GetPowerPanelByPropertyId<PowerPanelProjection>(id, userId);
            if (projection == null)
            {
                return this.Forbid();
            }

            var powerPanel = this._mapper.Map<PowerPanelViewModel>(projection);

            return this.View(powerPanel);
        }

    }
}
