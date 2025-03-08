namespace EnergySystem.Web.ViewModels.Battery
{
    using System.ComponentModel;

    using Data.Models;

    using Services.Mapping;

    using Battery=global::Battery;

    public class BatteryViewModel : IMapFrom<Battery>

    {
        public string Id { get; set; }

        public string Model { get; set; }
        
        [DisplayName("Capacity (kWh)")]
        public decimal Capacity { get; set; }
        [DisplayName("Voltage (V)")]
        public decimal Voltage { get; set; }
        [DisplayName("Current Charge Level of the battery (%)")]
        public decimal CurrentChargeLevel { get; set; }
        [DisplayName("State of Health (%)")]
        public decimal StateOfHealth { get; set; }
        [DisplayName("Cycle count")]
        public int CycleCount { get; set; }
    }
}
