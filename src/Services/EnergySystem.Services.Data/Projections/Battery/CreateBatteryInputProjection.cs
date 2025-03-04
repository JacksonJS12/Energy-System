namespace EnergySystem.Services.Data.Projections.Battery
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EnergySystem.Services.Data.Projections.Property;
    using EnergySystem.Web.ViewModels.Property;

    using static EnergySystem.Common.EntityValidationConstants.Battery;

    public class CreateBatteryInputProjection : BaseBatteryInputProjection
    {
        public IEnumerable<SinglePropertyProjection> Properties { get; set; } = new HashSet<SinglePropertyProjection>();
    }

}
