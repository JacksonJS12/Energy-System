namespace EnergySystem.Web.ViewModels.ApplicationUser
{
    using System.Collections.Generic;

    using Property;

    public class UserPropertiesViewModel
    {
        public IEnumerable<PropertyDetailsViewModel> Properties { get; set; }
    }
}
