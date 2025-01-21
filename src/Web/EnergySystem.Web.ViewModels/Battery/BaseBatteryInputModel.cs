namespace EnergySystem.Web.ViewModels.Battery
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static EnergySystem.Common.EntityValidationConstants.Battery;

    public class BaseBatteryInputModel
    {
        [Required]
        [StringLength(MaxModelLength, MinimumLength = MinModelLength)]
        public string Model { get; set; }

        [Required]
        [Range(MinCapacity, MaxCapacity)]
        public float Capacity { get; set; }

        [Required]
        [Range(MinVoltage, MaxVoltage)]
        public float Voltage { get; set; }

        [Required]
        [StringLength(MaxManufacturerLength, MinimumLength = MinManufacturerLength)]
        public string Manufacturer { get; set; }

        [Required]
        public DateTime ManufactureDate { get; set; }
        

        [Required]
        public DateTime InitialInstallation { get; set; }

        [Required]
        [Range(MinCurrentChargeLevel, MaxCurrentChargeLevel)]
        public float CurrentChargeLevel { get; set; }

        [Required]
        [Range(MinStateOfHealth, MaxStateOfHealth)]
        public float StateOfHealth { get; set; }

        [Required]
        public int CycleCount { get; set; }

        [Required]
        [Range(MinTemperature, MaxTemperature)]
        public float Temperature { get; set; }

        [Required]
        public decimal LifetimeEnergyStored { get; set; }

        [Required]
        public string PropertyId { get; set; }
    }
}
