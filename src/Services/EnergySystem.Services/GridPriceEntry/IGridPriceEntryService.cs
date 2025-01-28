namespace EnergySystem.Services.GridPriceEntry
{
    using System.Threading.Tasks;

    public interface IGridPriceEntryService
    {
        Task AddHourlyEntryAsync(string propertyId, int hour, decimal electricityUsed);
        Task<decimal> CalculateDailyCostAsync(string propertyId);
        Task ClearDailyEntriesAsync(string propertyId);
    }
}
