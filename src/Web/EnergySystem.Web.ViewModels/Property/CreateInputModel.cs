namespace EnergySystem.Web.ViewModels.Property
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EnergySystem.Web.ViewModels.Battery;
    using EnergySystem.Web.ViewModels.Grid;

    using static EnergySystem.Common.EntityValidationConstants.Property;

    public class CreateInputModel
    {
        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(MaxAddressLength, MinimumLength = MinAddressLength)]
        public string Address { get; set; }

        [Required]
        [StringLength(MaxElectricityNeedLength, MinimumLength = MinElectricityNeedLength)]
        public string ElectricityNeed { get; set; }

        [Required]
        public string GridId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> GridsItems { get; set; }

        public IEnumerable<BatteryViewModel> Batteries { get; set; } = new HashSet<BatteryViewModel>();
    }
}
