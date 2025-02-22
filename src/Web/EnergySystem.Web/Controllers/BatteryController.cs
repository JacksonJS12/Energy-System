namespace EnergySystem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EnergySystem.Services.Data.Battery;
    using EnergySystem.Services.Data.Property;
    using EnergySystem.Web.ViewModels.Battery;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Property;

    public class BatteryController : BaseController
    {
        private readonly IPropertyService _propertyService;
        private readonly IBatteryService _batteryService;
        public BatteryController(IPropertyService propertyService, IBatteryService batteryService)
        {
            this._propertyService = propertyService;
            this._batteryService = batteryService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult BatteryManagement(string id)
        {
            string userId = this.GetUserId();

            var battery = this._batteryService.GetById<SingleBatteryViewModel>(id, userId);
            if (battery == null)
            {
                return this.Forbid();
            }
            return this.View(battery);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(string propertyId)
        {
            string userId = this.GetUserId();

            var property = this._propertyService.GetById<SinglePropertyViewModel>(propertyId, userId);
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
            string userId = this.GetUserId();
            
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            try
            {
                await this._batteryService.CreateAsync(inputModel, inputModel.PropertyId);
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

            var inputModel = this._batteryService.GetById<EditBatteryInputModel>(id, userId);
            if (inputModel == null)
            {
                return this.Forbid();
            }
            return this.View(inputModel);
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

            try
            {
                await this._batteryService.UpdateAsync(input, id, userId);
            }
            catch (Exception)
            {
                return this.Forbid();
            }
            return this.RedirectToAction("BatteryManagement", "Battery", new { id });
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            string userId = this.GetUserId();

            var battery = this._batteryService.GetById<SingleBatteryViewModel>(id, userId);
            
            try
            {
                await this._batteryService.DeleteAsync(id, userId);
            }
            catch (Exception e)
            {
                return this.Forbid();
            }
            return this.RedirectToAction("Details", "Property", new { id = battery.PropertyId });
        }
    }
}
