namespace EnergySystem.Services.Projections.Battery
{

    using Services.Mapping;

    using Battery=global::Battery;


    public class BatteryProjection : IMapFrom<Battery>

    {
        public string Id { get; set; }

        public string Model { get; set; }

        public decimal Capacity { get; set; }

        public decimal Voltage { get; set; }

        public decimal CurrentChargeLevel { get; set; }

        public decimal StateOfHealth { get; set; }

        public int CycleCount { get; set; }
    }
}
