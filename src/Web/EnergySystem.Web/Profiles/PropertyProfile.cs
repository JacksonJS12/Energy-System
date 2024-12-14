namespace EnergySystem.Web.Profiles
{
    using AutoMapper;

    using Services.Projections;

    using ViewModels.Battery;
    using ViewModels.Property;

    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            this.CreateMap<PropertyProjection, PropertyDetailsViewModel>();
            this.CreateMap<BatteryProjection, BatteryViewModel>();
        }
    }
}
