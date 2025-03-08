namespace EnergySystem.Web.ViewModels.Battery
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using static EnergySystem.Common.EntityValidationConstants.Battery;

    public class BaseBatteryInputModel
    {
        [Required]
        [StringLength(MaxModelLength, MinimumLength = MinModelLength)]
        public string Model { get; set; }

        [Required]
        [Range(MinCapacity, MaxCapacity)]
        [DisplayName("Capacity (kWh)")]
        public decimal Capacity { get; set; }

        [Required]
        [Range(MinVoltage, MaxVoltage)]
        [DisplayName("Voltage (V)")]
        public decimal Voltage { get; set; }

        [Required]
        [StringLength(MaxManufacturerLength, MinimumLength = MinManufacturerLength)]
        public string Manufacturer { get; set; }

        [Required]
        [DisplayName("Date Manufactured")]
        public DateTime ManufactureDate { get; set; }

        [Required]
        [DisplayName("Initial Installation Date")]
        public DateTime InitialInstallation { get; set; }

        [Required]
        [Range(MinCurrentChargeLevel, MaxCurrentChargeLevel)]
        [DisplayName("Current Charge Level of the battery (%)")]
        public decimal CurrentChargeLevel { get; set; }

        [Required]
        [Range(MinStateOfHealth, MaxStateOfHealth)]
        [DisplayName("State of Health (%)")]
        public decimal StateOfHealth { get; set; }

        [Required]
        [DisplayName("Cycle count")]
        public int CycleCount { get; set; }

        [Required]
        [Range(MinTemperature, MaxTemperature)]
        [DisplayName("Temperature (\u00b0C)")]
        public decimal Temperature { get; set; }

        [Required]
        [DisplayName("Lifetime Energy Stored")]
        public decimal LifetimeEnergyStored { get; set; }

        [Required]
        public string PropertyId { get; set; }
    }
}
