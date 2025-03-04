namespace EnergySystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using EnergySystem.Services.Data.MarketPrice;
    using EnergySystem.Web.ViewModels.MarketPrice;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Projections.MarketPrice;

    public class PriceController : BaseController
    {
        private readonly IMarketPriceService _marketPriceService;
        private readonly IMapper _mapper;
        public PriceController(IMarketPriceService marketPriceService, IMapper mapper)
        {
            this._marketPriceService = marketPriceService;
            this._mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> PriceTracker(DateTime? selectedDate)
        {
            // var viewModel = this._marketPriceService.GetAll<MarketPriceInListViewModel>();
            // return this.View(viewModel);
            
            // Default to today's date if no date is selected
            selectedDate ??= DateTime.UtcNow.Date;

            var marketPrices = await this._marketPriceService.GetAll<MarketPriceInListProjection>(selectedDate.Value);

            List<MarketPriceInListProjection> projections = marketPrices.Select(mp => new MarketPriceInListProjection
            {
                Hour = mp.Hour,
                PricePerKWh = mp.PricePerKWh
            }).ToList();

            this.ViewBag.SelectedDate = selectedDate.Value.ToString("yyyy-MM-dd");

            List<MarketPriceInListViewModel> viewModel = this._mapper.Map<List<MarketPriceInListViewModel>>(projections);

            return this.View(viewModel);
        }
        
        [Authorize]
        public IActionResult CostAnalysis() => this.View();
    }
}
