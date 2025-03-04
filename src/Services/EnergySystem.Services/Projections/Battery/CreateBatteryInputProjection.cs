namespace EnergySystem.Services.Projections.Battery
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Property;

    using static EnergySystem.Common.EntityValidationConstants.Battery;

    public class CreateBatteryInputProjection : BaseBatteryInputProjection
    {
        public IEnumerable<SinglePropertyProjection> Properties { get; set; } = new HashSet<SinglePropertyProjection>();
    }

}
