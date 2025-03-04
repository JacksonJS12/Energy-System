namespace EnergySystem.Services.Data.Projections.ApplicationUser
{
    using System.Collections.Generic;

    using EnergySystem.Web.ViewModels.Property;

    public class UserPropertiesProjection
    {
        public ICollection<SinglePropertyProjection> Properties { get; set; }
    }
}
