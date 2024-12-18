﻿namespace EnergySystem.Web.ViewModels.Property
{
    using System.Collections.Generic;

    using Battery;

    using Data.Models;

    using Grid;

    using Services.Mapping;

    public class PropertyDetailsViewModel : IMapFrom<Property>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public float ElectricityNeed { get; set; }

        public GridViewModel Grid { get; set; }

        public IEnumerable<BatteryViewModel> Batteries { get; set; }
    }
}
