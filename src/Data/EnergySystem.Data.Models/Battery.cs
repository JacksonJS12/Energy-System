using System;

using EnergySystem.Data.Common.Models;
using EnergySystem.Data.Models;

using Microsoft.EntityFrameworkCore;

public class Battery : BaseDeletableModel<string>
{
    public Battery()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public string Model { get; set; } //Battery model number
    [Precision(18, 2)]
    public decimal Capacity { get; set; } //Energy storage capacity in Wh or Ah
    [Precision(18, 2)]
    public decimal Voltage { get; set; } //Nominal voltage

    public string Manufacturer { get; set; } //Manufacturer name

    public DateTime ManufactureDate { get; set; } //Date of manufacture

    public DateTime InitialInstallation { get; set; } //Date of installation
    [Precision(18, 2)]
    public decimal CurrentChargeLevel { get; set; } //Real-time charge level (%)
    [Precision(18, 2)]
    public decimal StateOfHealth { get; set; } //Battery health as a percentage

    public int CycleCount { get; set; } //Number of charge-discharge cycles
    [Precision(18, 2)]
    public decimal Temperature { get; set; } //Current temperature in °C
    [Precision(18, 2)]
    public decimal LifetimeEnergyStored { get; set; } //Total energy stored (Wh)
    [Precision(18, 2)]
    public decimal LifetimeEnergyStoredAtStartOfDay { get; set; } //Energy stored at the start of the day (for daily calculations)

    public DateTime WarrantyExpirationDate { get; set; } //Warranty expiration date
    [Precision(18, 2)]
    public decimal PriceChargingAt { get; set; }

    public string PropertyId { get; set; }

    public virtual Property Property { get; set; }
}
