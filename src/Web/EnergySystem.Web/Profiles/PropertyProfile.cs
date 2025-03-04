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
            CreateMap<Property, EditPropertyInputProjection>();
        }
    }
}
