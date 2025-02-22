namespace EnergySystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Data.Common.Repositories;

    using Microsoft.AspNetCore.Mvc;

    using Services.Data.MarketPrice;

    using ViewModels.MarketPrice;

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

            // Fetch data for the selected day
            var marketPrices = await this._marketPriceService.GetAll<MarketPriceInListViewModel>(selectedDate.Value);

            // Map to ViewModel
            var viewModel = marketPrices.Select(mp => new MarketPriceInListViewModel
            {
                Hour = mp.Hour,
                PricePerKWh = mp.PricePerKWh
            }).ToList();

            ViewBag.SelectedDate = selectedDate.Value.ToString("yyyy-MM-dd");

            return View(viewModel);
        }

        public IActionResult CostAnalysis() => this.View();
    }
}
