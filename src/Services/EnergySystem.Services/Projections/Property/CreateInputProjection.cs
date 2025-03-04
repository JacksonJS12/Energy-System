namespace EnergySystem.Services.Projections.Property
{
    using System.Collections.Generic;

    using Battery;

    using static EnergySystem.Common.EntityValidationConstants.Property;

    public class CreateInputProjection : BasePropertyInputProjection
    {
        public IEnumerable<BatteryProjection> Batteries { get; set; } = new HashSet<BatteryProjection>();
    }
}
