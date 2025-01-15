namespace EnergySystem.Web.ViewModels.Battery
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EnergySystem.Web.ViewModels.Property;

    using static EnergySystem.Common.EntityValidationConstants.Battery;

    public class CreateBatteryInputModel : BaseBatteryInputModel
    {
        public IEnumerable<SinglePropertyViewModel> Properties { get; set; } = new HashSet<SinglePropertyViewModel>();
    }

}
