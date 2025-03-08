namespace EnergySystem.Web.ViewModels.Property
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using EnergySystem.Web.ViewModels.Battery;

    using static EnergySystem.Common.EntityValidationConstants.Property;


    public class BasePropertyInputModel
    {
        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(MaxAddressLength, MinimumLength = MinAddressLength)]
        public string Address { get; set; }

        [Required]
        [DisplayName("Electricity need for a month")]
        public decimal ElectricityNeed { get; set; }

        [Required]
        [DisplayName("Grid")]
        public string GridId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> GridsItems { get; set; }
    }
}
