namespace EnergySystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Data.Models;

    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Battery;
    using Services.Data.Property;

    using ViewModels.Battery;

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
        public IActionResult BatteryManagement(string id)
        {
            var battery = this._batteryService.GetById<SingleBatteryViewModel>(id);

            return this.View(battery);
        }

        [HttpGet]
        public IActionResult Create(string propertyId)
        {
            var inputModel = new CreateBatteryInputModel
            {
                PropertyId = propertyId // Pass the property ID to the view
            };
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBatteryInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            await this._batteryService.CreateAsync(inputModel, inputModel.PropertyId);
            return RedirectToAction("Details", "Property", new { id = inputModel.PropertyId });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var inputModel = this._batteryService.GetById<EditBatteryInputModel>(id);
            return this.View(inputModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(EditBatteryInputModel input, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this._batteryService.UpdateAsync(input, id);
            return this.RedirectToAction("BatteryManagement", "Battery", new {id});
        }
        public async Task<IActionResult> Delete(string id)
        {
            var battery = this._batteryService.GetById<SingleBatteryViewModel>(id);
            await this._batteryService.DeleteAsync(id);
            return this.RedirectToAction("Details", "Property", new { id = battery.PropertyId });
        }
    }
}
