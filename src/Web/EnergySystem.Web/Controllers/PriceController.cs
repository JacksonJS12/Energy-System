namespace EnergySystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using EnergySystem.Services.Data.MarketPrice;
    using EnergySystem.Web.ViewModels.MarketPrice;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PriceController : BaseController
    {
        private readonly IMarketPriceService _marketPriceService;
        private readonly IMapper _mapper;
        public PriceController(IMarketPriceService marketPriceService)
        {
            this._marketPriceService = marketPriceService;
        }
        
        [HttpGet]
        public async Task<IActionResult> PriceTracker(DateTime? selectedDate)
        {
            // var viewModel = this._marketPriceService.GeAll<MarketPriceInListViewModel>();
            // return this.View(viewModel);
            
            // Default to today's date if no date is selected
            selectedDate ??= DateTime.UtcNow.Date;

            var marketPrices = await this._marketPriceService.GetAll<MarketPriceInListViewModel>(selectedDate.Value);

            var viewModel = marketPrices.Select(mp => new MarketPriceInListViewModel
            {
                Hour = mp.Hour,
                PricePerKWh = mp.PricePerKWh
            }).ToList();

            this.ViewBag.SelectedDate = selectedDate.Value.ToString("yyyy-MM-dd");

            return this.View(viewModel);
        }
        
        [Authorize]
        public IActionResult CostAnalysis() => this.View();
    }
}
