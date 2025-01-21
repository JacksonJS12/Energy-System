namespace EnergySystem.Services.Background.GridPriceEntry
{
    using System.Threading.Tasks;

    public interface IGridPriceEntryService
    {
        Task AddHourlyEntryAsync(string propertyId, int hour, decimal electricityUsed, string region = null);
        Task<decimal> CalculateDailyCostAsync(string propertyId);
        Task ClearDailyEntriesAsync(string propertyId);
    }
}
