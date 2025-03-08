namespace EnergySystem.Web.ViewModels.Battery
{
    using System;
    using System.ComponentModel;

    using EnergySystem.Data.Models;

    using EnergySystem.Services.Mapping;

    using Battery=global::Battery;

    public class SingleBatteryViewModel : IMapFrom<Battery>
    {
        public string Id { get; set; }
        public string Model { get; set; } //Battery model number
        [DisplayName("Capacity (kWh)")]
        public decimal Capacity { get; set; } //Energy storage capacity in Wh or Ah
        [DisplayName("Voltage (V)")]
        public decimal Voltage { get; set; } //Nominal voltage
        public string Manufacturer { get; set; } //	Manufacturer name
        [DisplayName("Date Manufactured")]
        public DateTime ManufactureDate { get; set; } //Date of manufacture
        [DisplayName("Initial Installation Date")]
        public DateTime InitialInstallation { get; set; } //	Date of installation
        [DisplayName("Current Charge Level of the battery (%)")]
        public decimal CurrentChargeLevel { get; set; } //Real-time charge level (%)
        [DisplayName("State of Health (%)")]
        public decimal StateOfHealth { get; set; } //	Battery health as a percentage
        [DisplayName("Cycle count")]
        public int CycleCount { get; set; } //	Number of charge-discharge cycles

        [DisplayName("Temperature (\u00b0C)")]
        public decimal Temperature { get; set; } //	Current temperature in °C

        [DisplayName("Lifetime Energy Stored")]
        public decimal LifetimeEnergyStored { get; set; }
        
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
    }
}
