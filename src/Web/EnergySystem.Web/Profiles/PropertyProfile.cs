namespace EnergySystem.Web.Profiles
{
    using AutoMapper;

    using Data.Models;

    using Services.Projections.Property;

    using ViewModels.Property;

    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            this.CreateMap<CreateInputProjection, CreateInputModel>();
            this.CreateMap<EditPropertyInputProjection, EditPropertyInputModel>();
            this.CreateMap<PropertiesListProjection, PropertiesListViewModel>();
            this.CreateMap<PropertyInListProjection, PropertyInListViewModel>();
            this.CreateMap<SinglePropertyProjection, SinglePropertyViewModel>();

            this.CreateMap<CreateInputModel, CreateInputProjection>();
            this.CreateMap<EditPropertyInputModel, EditPropertyInputProjection>();
            this.CreateMap<PropertiesListViewModel, PropertiesListProjection>();
            this.CreateMap<PropertyInListViewModel, PropertyInListProjection>();
            this.CreateMap<SinglePropertyViewModel, SinglePropertyProjection>();

            this.CreateMap<Property, PropertyInListProjection>();
            this.CreateMap<Property, SinglePropertyProjection>();
            this.CreateMap<Property, EditPropertyInputProjection>();
            
            this.CreateMap<PowerPanel, PowerPanelProjection>();
            this.CreateMap<PowerPanelProjection, PowerPanelViewModel>();
            this.CreateMap<PowerPanelViewModel, PowerPanelProjection>();
            this.CreateMap<PowerPanel, PowerPanelProjection>()
                .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.Property.Name))
                .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(src => src.Property.Id));
        }
    }
}
