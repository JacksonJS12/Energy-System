namespace EnergySystem.Services.Data.Projections.Property
{
    using System.Collections.Generic;

    using Battery;

    using EnergySystem.Web.ViewModels.Battery;

    using static EnergySystem.Common.EntityValidationConstants.Property;

    public class CreateInputProjection : BasePropertyInputProjection
    {
        public IEnumerable<BatteryProjection> Batteries { get; set; } = new HashSet<BatteryProjection>();
    }
}
