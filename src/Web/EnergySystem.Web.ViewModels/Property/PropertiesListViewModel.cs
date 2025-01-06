namespace EnergySystem.Web.ViewModels.Property
{
    using System.Collections.Generic;

    public class PropertiesListViewModel
    {
        public IEnumerable<PropertyInListViewModel> Properties { get; set; }
    }
}
