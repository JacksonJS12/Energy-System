namespace EnergySystem.Services.Projections.User
{
    using System.Collections.Generic;

    using Property;


    public class UserPropertiesProjection
    {
        public ICollection<SinglePropertyProjection> Properties { get; set; }
    }
}
