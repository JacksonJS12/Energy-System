namespace EnergySystem.Web.Profiles
{
    using AutoMapper;
    using EnergySystem.Data.Models;
    using EnergySystem.Services.Projections.Grid;

    using ViewModels.Grid;

    public class GridProfile : Profile
    {
        public GridProfile()
        {
            this.CreateMap<Grid, GridProjection>();
            this.CreateMap<GridViewModel, GridProjection>();
            this.CreateMap<GridProjection, GridViewModel>();
        }
    }
}
