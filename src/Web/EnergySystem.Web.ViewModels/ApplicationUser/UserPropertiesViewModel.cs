namespace EnergySystem.Web.ViewModels.ApplicationUser
{
    using System.Collections.Generic;

    using EnergySystem.Web.ViewModels.Property;

    public class UserPropertiesViewModel
    {
        public ICollection<SinglePropertyViewModel> Properties { get; set; }
    }
}
