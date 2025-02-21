namespace ConsoleApp1;

using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://ibex.bg/dam-data-chart-2/"; // Update with the actual page URL
        string targetDate = "2025-01-01"; // The date to filter

        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);
        
        // Load the webpage
        var document = await context.OpenAsync(url);
        

        // Find the date filter input field
        var dateInput = document.QuerySelector("input.dateFilter") as IHtmlInputElement;
        if (dateInput == null)
        {
            Console.WriteLine("Date input field not found!");
            return;
        }

        // Update the date value
        dateInput.Value = targetDate;

        // Find and submit the form
        var form = document.QuerySelector("form") as IHtmlFormElement;
        if (form == null)
        {
            Console.WriteLine("Form not found!");
            return;
        }

        
        // Submit the form with the new date
        var resultDocument = await form.SubmitAsync();

        // Extract chart data (assuming it's inside a table with ID 'dam-table')
        var table = resultDocument.QuerySelector("#dam-table");
        if (table == null)
        {
            Console.WriteLine("Table with chart data not found!");
            return;
        }

        // Extract rows of the table
        var rows = table.QuerySelectorAll("tr").Skip(1); // Skip the header row

        Console.WriteLine($"DAM Data Chart for {targetDate}:\n");
        
        var priceList = document.QuerySelectorAll("td.column-price")
            .Select(x => Decimal.Parse(x.InnerHtml))
            .ToArray();
        var hourList = document.QuerySelectorAll("td.column-time_part")
            .Select(x => TimeSpan.Parse(x.InnerHtml)) // Parse as TimeSpan (e.g., "00:00:00")
            .ToArray();
        var dateList = document.QuerySelectorAll("td.column-date_part")
            .Select(x => DateTime.Parse(x.InnerHtml)) // Parse as DateTime
            .ToArray();
        for (int i = 0; i < 23; i++)
        {
            Console.WriteLine($"Hour: {hourList[i]}, Price (BGN): {priceList[i]}");
        }
    }
}
