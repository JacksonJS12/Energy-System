namespace EnergySystem.Web.ViewModels.Property
{
    using System.Collections.Generic;

    using EnergySystem.Web.ViewModels.Battery;

    using static EnergySystem.Common.EntityValidationConstants.Property;

    public class CreateInputModel : BasePropertyInputModel
    {
        public IEnumerable<BatteryViewModel> Batteries { get; set; } = new HashSet<BatteryViewModel>();
    }
}
