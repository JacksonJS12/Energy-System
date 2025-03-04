﻿namespace EnergySystem.Services.Projections.Battery
{

    using Services.Mapping;

    using Battery=global::Battery;

    public class EditBatteryInputProjection : BaseBatteryInputProjection, IMapFrom<Battery>
    {
        public string Id { get; set; }
    }
}
