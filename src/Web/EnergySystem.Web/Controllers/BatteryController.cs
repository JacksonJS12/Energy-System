namespace EnergySystem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using EnergySystem.Web.ViewModels.Battery;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Battery;
    using Services.Projections.Battery;
    using Services.Projections.Property;
    using Services.Property;

    using ViewModels.Property;

    public class BatteryController : BaseController
    {
        private readonly IPropertyService _propertyService;
        private readonly IBatteryService _batteryService;
        private readonly IMapper _mapper;
        public BatteryController(IPropertyService propertyService, IBatteryService batteryService, IMapper mapper)
        {
            this._propertyService = propertyService;
            this._batteryService = batteryService;
            this._mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult BatteryManagement(string id)
        {
            string userId = this.GetUserId();

            var battery = this._batteryService.GetById<SingleBatteryProjection>(id, userId);
            if (battery == null)
            {
                return this.Forbid();
            }

            SingleBatteryViewModel viewModel = this._mapper.Map<SingleBatteryViewModel>(battery);
            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(string propertyId)
        {
            string userId = this.GetUserId();

            var property = this._propertyService.GetById<SinglePropertyProjection>(propertyId, userId);
            if (property == null)
            {
                return this.Forbid();
            }

            var inputModel = new CreateBatteryInputModel
            {
                PropertyId = propertyId // Pass the property ID to the view
            };
            return View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateBatteryInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            CreateBatteryInputProjection projection = this._mapper.Map<CreateBatteryInputProjection>(inputModel);

            try
            {
                await this._batteryService.CreateAsync(projection, inputModel.PropertyId);
            }
            catch (Exception)
            {
                return this.Forbid();
            }
            return this.RedirectToAction("Details", "Property", new { id = inputModel.PropertyId });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(string id)
        {
            string userId = this.GetUserId();

            var projection = this._batteryService.GetById<EditBatteryInputProjection>(id, userId);
            if (projection == null)
            {
                return this.Forbid();
            }

            EditBatteryInputModel viewModel = this._mapper.Map<EditBatteryInputModel>(projection);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditBatteryInputModel input, string id)
        {
            string userId = this.GetUserId();

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            EditBatteryInputProjection projection = this._mapper.Map<EditBatteryInputProjection>(input);

            try
            {
                await this._batteryService.UpdateAsync(projection, id, userId);
            }
            catch (Exception)
            {
                return this.Forbid();
            }
            return this.RedirectToAction("BatteryManagement", "Battery", new { id });
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            string userId = this.GetUserId();

            var battery = this._batteryService.GetById<SingleBatteryProjection>(id, userId);
            if (battery == null)
            {
                return this.Forbid();
            }
            var propertyId = battery.PropertyId;

            await this._batteryService.DeleteAsync(id);


            return this.RedirectToAction("Details", "Property", new { id = propertyId });
        }
    }
}
