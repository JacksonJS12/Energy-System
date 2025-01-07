﻿namespace EnergySystem.Web.ViewModels.Property
{

    using EnergySystem.Data.Models;
    using EnergySystem.Services.Mapping;

    public class PropertyInListViewModel : IMapFrom<Property>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public float ElectricityNeed { get; set; }
    }
}