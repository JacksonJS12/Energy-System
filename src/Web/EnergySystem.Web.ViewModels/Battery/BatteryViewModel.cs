﻿namespace EnergySystem.Web.ViewModels.Battery
{
    public class BatteryViewModel
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
