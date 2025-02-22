using System;

namespace ConsoleApp2
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergySystem.Data.Models;

    class Program
    {
        static void Main(string[] args)
        {
            MarketPrice record1 = new MarketPrice()
            {
                Hour = 1,
                Date = DateTime.Parse("2025-02-22"),
                PricePerKWh = 0.33m
            };
            MarketPrice record2 = new MarketPrice()
            {
                Hour = 10,
                Date = DateTime.Parse("2025-02-22"),
                PricePerKWh = 0.123m
            };
            MarketPrice record3 = new MarketPrice()
            {
                Hour = 4,
                Date = DateTime.Parse("2025-02-22"),
                PricePerKWh = 0.65m
            };
            DateTime date = DateTime.UtcNow.Date.AddHours(3);
            List<MarketPrice> prices = new List<MarketPrice>() { record1, record2, record3 };
            string result = prices.FirstOrDefault().Date.Date == date.Date.Date ? "Today's chart" : "Day ahead chart";
            Console.WriteLine(result);
        }
    }
}
