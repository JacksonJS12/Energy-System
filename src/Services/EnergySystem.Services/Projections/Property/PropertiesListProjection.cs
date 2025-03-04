namespace EnergySystem.Services.Projections.Property
{
    using System.Collections.Generic;


    public class PropertiesListProjection
    {
        public IEnumerable<PropertyInListProjection> Properties { get; set; }
    }
}
