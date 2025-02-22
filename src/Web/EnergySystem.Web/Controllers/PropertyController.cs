namespace EnergySystem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EnergySystem.Services.Data.Grid;
    using EnergySystem.Services.Data.Property;
    using EnergySystem.Web.ViewModels.Property;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PropertyController : BaseController
    {
        private readonly IGridService _gridService;
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService, IGridService gridService)
        {
            this._propertyService = propertyService;
            this._gridService = gridService;
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

            await this._propertyService.CreateAsync(input, userId);

            return this.RedirectToAction("All", "Property");
        }

        [HttpGet]
        [Authorize]
        public IActionResult All()
        {
            string userId = this.GetUserId();

            var viewModel = new PropertiesListViewModel
            {
                Properties = this._propertyService.GeAll<PropertyInListViewModel>(userId),
            };
            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(string id)
        {
            string userId = this.GetUserId();

            var property = this._propertyService.GetById<SinglePropertyViewModel>(id, userId);
            if (property == null)
            {
                return this.Forbid();
            }

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

            var inputModel = this._propertyService.GetById<EditPropertyInputModel>(id, userId);

            if (inputModel == null)
            {
                return this.Forbid();
            }

            inputModel.GridsItems = this._gridService.GetAllAsKeyValuePairs();

            return this.View(inputModel);
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

            try
            {
                await this._propertyService.UpdateAsync(input, id, userId);
            }
            catch (Exception)
            {
                return this.Forbid();
            }

            return this.RedirectToAction("Details", "Property", new { id });
        }
    }
}
