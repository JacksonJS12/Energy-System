namespace EnergySystem.Web.Profiles
{
    using System.Collections.Generic;

    using AutoMapper;

    using Data.Models;

    using Services.Projections.MarketPrice;
    using ViewModels.MarketPrice;

    public class MarketPriceProfile : Profile
    {
        public MarketPriceProfile()
        {
            this.CreateMap<MarketPriceInListProjection, MarketPriceInListViewModel>();
            this.CreateMap<MarketPriceInListViewModel,MarketPriceInListProjection >();

            this.CreateMap<MarketPricesListProjection, MarketPricesListViewModel>();
            this.CreateMap<MarketPricesListViewModel, MarketPricesListProjection>();

            this.CreateMap<MarketPrice, MarketPriceInListViewModel>();
            CreateMap<MarketPrice, MarketPriceInListProjection>();

        }
    }
}
