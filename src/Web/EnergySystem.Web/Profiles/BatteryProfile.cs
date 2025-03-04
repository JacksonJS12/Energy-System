namespace EnergySystem.Web.Profiles
{
    using AutoMapper;
    using EnergySystem.Services.Projections.Property;
    using Services.Projections.Battery;

    using ViewModels.Battery;
    using ViewModels.Property;

    public class BatteryProfile : Profile
    {
        public BatteryProfile()
        {
            this.CreateMap<CreateBatteryInputProjection, CreateBatteryInputModel>();
            this.CreateMap<CreateBatteryInputModel, CreateBatteryInputProjection>();

            this.CreateMap<Battery, BatteryProjection>();
            this.CreateMap<BatteryProjection, BatteryViewModel>();
            this.CreateMap<Battery, SingleBatteryProjection>();
            this.CreateMap<SingleBatteryProjection, SingleBatteryViewModel>();
            this.CreateMap<Battery, EditBatteryInputProjection>();
            this.CreateMap<EditBatteryInputProjection, EditBatteryInputModel>();
            this.CreateMap<EditBatteryInputModel, EditBatteryInputProjection>();
        }
    }
}
