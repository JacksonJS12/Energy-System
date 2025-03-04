namespace EnergySystem.Services.Data.Projections.Battery
{
    using System;


    using EnergySystem.Services.Mapping;

    using Battery=global::Battery;

    public class SingleBatteryProjection : IMapFrom<Battery>
    {
        public string Id { get; set; }
        public string Model { get; set; } //Battery model number
        public decimal Capacity { get; set; } //Energy storage capacity in Wh or Ah

        public decimal Voltage { get; set; } //Nominal voltage
        public string Manufacturer { get; set; } //	Manufacturer name

        public DateTime ManufactureDate { get; set; } //Date of manufacture

        public DateTime InitialInstallation { get; set; } //	Date of installation

        public decimal CurrentChargeLevel { get; set; } //Real-time charge level (%)

        public decimal StateOfHealth { get; set; } //	Battery health as a percentage

        public int CycleCount { get; set; } //	Number of charge-discharge cycles

        public decimal Temperature { get; set; } //	Current temperature in °C

        public decimal LifetimeEnergyStored { get; set; } //Total energy stored (Wh)

        public decimal LifetimeEnergyDelivered { get; set; } //	Total energy delivered (Wh)

        public DateTime WarrantyExpirationDate { get; set; } //	Warranty expiration date
        
        public string PropertyId { get; set; }
    }
}
