namespace EnergySystem.Services.Mapping.Profiles
{
    using System.Collections.Generic;

    using AutoMapper;

    using Projections;

    using Web.ViewModels.Grid;

    public class GridProfile : Profile
    {
        public GridProfile()
        {
            this.CreateMap<GridViewModel, GridProjection>();
            this.CreateMap<IEnumerable<GridViewModel>, IEnumerable<GridProjection>>();
        }
    }
}
