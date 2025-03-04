namespace EnergySystem.Services.Data.Projections.Property
{
    using System.Collections.Generic;


    public class PropertyListProjection
    {
        public IEnumerable<PropertyInListProjection> Properties { get; set; }
    }
}
