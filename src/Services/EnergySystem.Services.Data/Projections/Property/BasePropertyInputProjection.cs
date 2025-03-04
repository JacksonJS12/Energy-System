namespace EnergySystem.Services.Data.Projections.Property
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static EnergySystem.Common.EntityValidationConstants.Property;


    public class BasePropertyInputProjection
    {
        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(MaxAddressLength, MinimumLength = MinAddressLength)]
        public string Address { get; set; }

        [Required]
        public decimal ElectricityNeed { get; set; }

        [Required]
        public string GridId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> GridsItems { get; set; }
    }
}
