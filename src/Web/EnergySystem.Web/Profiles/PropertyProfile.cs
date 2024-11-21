namespace EnergySystem.Web.Profiles
{
    using AutoMapper;

    using EnergySystem.Services.Projections;

    using EnergySystem.Web.ViewModels.Battery;
    using EnergySystem.Web.ViewModels.Grid;
    using EnergySystem.Web.ViewModels.Property;

    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            this.CreateMap<PropertyProjection, PropertyDetailsViewModel>();
            this.CreateMap<GridProjection, GridViewModel>();
            this.CreateMap<BatteryProjection, BatteryViewModel>();
        }
    }
}
