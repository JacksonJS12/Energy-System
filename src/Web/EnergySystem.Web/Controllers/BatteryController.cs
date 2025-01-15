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
        private readonly IBatteryService  _batteryService;
        public BatteryController(IPropertyService propertyService, IBatteryService  batteryService)
        {
            this._propertyService = propertyService;
            this._batteryService = batteryService;
        }
        // GET
        public IActionResult BatteryManagement() => this.View();
        
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
        public async Task<IActionResult>  Create(CreateBatteryInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            await this._batteryService.CreateAsync(inputModel, inputModel.PropertyId);
            return RedirectToAction("Details", "Property", new { id = inputModel.PropertyId });
        }

    }
}
