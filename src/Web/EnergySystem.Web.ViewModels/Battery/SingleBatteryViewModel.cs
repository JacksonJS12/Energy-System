﻿namespace EnergySystem.Web.ViewModels.Battery
{
    using System;

    using EnergySystem.Data.Models;

    using EnergySystem.Services.Mapping;

    public class SingleBatteryViewModel : IMapFrom<Battery>
    {
        public string Id { get; set; }
        public string Model { get; set; } //Battery model number
        public float Capacity { get; set; } //Energy storage capacity in Wh or Ah

        public float Voltage { get; set; } //Nominal voltage
        public string Manufacturer { get; set; } //	Manufacturer name

        public DateTime ManufactureDate { get; set; } //Date of manufacture

        public DateTime InitialInstallation { get; set; } //	Date of installation

        public float CurrentChargeLevel { get; set; } //Real-time charge level (%)

        public float StateOfHealth { get; set; } //	Battery health as a percentage

        public int CycleCount { get; set; } //	Number of charge-discharge cycles

        public float Temperature { get; set; } //	Current temperature in °C

        public float LifetimeEnergyStored { get; set; } //Total energy stored (Wh)

        public float LifetimeEnergyDeliveredpe { get; set; } //	Total energy delivered (Wh)

        public DateTime WarrantyExpirationDate { get; set; } //	Warranty expiration date
        
        public string PropertyId { get; set; }
    }
}
