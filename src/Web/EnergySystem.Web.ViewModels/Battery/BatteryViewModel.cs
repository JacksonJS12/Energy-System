namespace EnergySystem.Web.ViewModels.Battery
{
    using Data.Models;

    using Services.Mapping;

    public class BatteryViewModel : IMapFrom<Battery>

    {
        public string Id { get; set; }

        public string Model { get; set; }

        public float Capacity { get; set; }

        public float Voltage { get; set; }

        public float CurrentChargeLevel { get; set; }

        public float StateOfHealth { get; set; }

        public int CycleCount { get; set; }
    }
}
