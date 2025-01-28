namespace EnergySystem.Services.WebScraper;

using AngleSharp;
using AngleSharp.Dom;

using Data.Models;

class Program
{
    static async Task Main(string[] args)
    {
        var config = Configuration.Default.WithDefaultLoader();
        var address = "https://ibex.bg/dam-data-chart-2/";
        var context = BrowsingContext.New(config);
        var document = await context.OpenAsync(address);
     
        var priceList = document.QuerySelectorAll("td.column-price").Select(x => Decimal.Parse(x.InnerHtml)).ToArray();
        var hourList = document.QuerySelectorAll("td.column-time_part").Select(x => x.InnerHtml).ToArray();
        var dateList = document.QuerySelectorAll("td.column-date_part").Select(x => DateTime.Parse(x.InnerHtml)).ToArray();

        HashSet<MarketPrice> marketPrices = new HashSet<MarketPrice>();
        MarketPrice marketPrice;
        for (int i = 0; i < 24; i++)
        {
            marketPrice = new MarketPrice()
            {
                Hour = hourList[i],
                PricePerKWh = priceList[i],
                Date = dateList[i]
            };
            marketPrices.Add(marketPrice);
        }

        foreach (var item in marketPrices)
        {
            Console.WriteLine($"{item.Date} - {item.Hour} - {item.PricePerKWh}");
        }
        
    }
}
