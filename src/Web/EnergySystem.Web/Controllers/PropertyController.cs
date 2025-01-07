﻿namespace EnergySystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using EnergySystem.Services.Data.Grid;
    using EnergySystem.Services.Data.Property;
    using EnergySystem.Web.ViewModels.ApplicationUser;
    using EnergySystem.Web.ViewModels.Property;

    using Microsoft.AspNetCore.Mvc;

    using Services.Mapping;

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
        public async Task<IActionResult> Create(CreateInputModel input)
        {
            var userId = this.GetUserId();

            await this._propertyService.CreateAsync(input, userId);

            return this.RedirectToAction("Property", "All");
        }

        [HttpGet]
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
        public IActionResult Details(string id)
        {
            var property = this._propertyService.GetById<SinglePropertyViewModel>(id);

            return this.View(property);
        }
    }
}
