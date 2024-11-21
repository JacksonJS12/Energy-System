namespace EnergySystem.Web.ViewModels.ApplicationUser
{
    using System.Collections.Generic;

    using EnergySystem.Web.ViewModels.Property;

    public class UserPropertiesViewModel
    {
        public IEnumerable<PropertyDetailsViewModel> Properties { get; set; }
    }
}
