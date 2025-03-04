namespace EnergySystem.Services.Data.Projections.Battery
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static EnergySystem.Common.EntityValidationConstants.Battery;

    public class BaseBatteryInputProjection
    {
        [Required]
        [StringLength(MaxModelLength, MinimumLength = MinModelLength)]
        public string Model { get; set; }

        [Required]
        [Range(MinCapacity, MaxCapacity)]
        public decimal Capacity { get; set; }

        [Required]
        [Range(MinVoltage, MaxVoltage)]
        public decimal Voltage { get; set; }

        [Required]
        [StringLength(MaxManufacturerLength, MinimumLength = MinManufacturerLength)]
        public string Manufacturer { get; set; }

        [Required]
        public DateTime ManufactureDate { get; set; }
        

        [Required]
        public DateTime InitialInstallation { get; set; }

        [Required]
        [Range(MinCurrentChargeLevel, MaxCurrentChargeLevel)]
        public decimal CurrentChargeLevel { get; set; }

        [Required]
        [Range(MinStateOfHealth, MaxStateOfHealth)]
        public decimal StateOfHealth { get; set; }

        [Required]
        public int CycleCount { get; set; }

        [Required]
        [Range(MinTemperature, MaxTemperature)]
        public decimal Temperature { get; set; }

        [Required]
        public decimal LifetimeEnergyStored { get; set; }

        [Required]
        public string PropertyId { get; set; }
    }
}
