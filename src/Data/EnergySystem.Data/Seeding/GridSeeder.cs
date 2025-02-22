namespace EnergySystem.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Models;

    internal class GridSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Grids.AnyAsync())
            {
                return; // Data already seeded
            }

            var grids = new[]
            {
                new Grid
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "IBEX",
                    CurrentUsage = 0m,
                    SupplyPrice = 0m,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = null,
                    IsDeleted = false,
                    DeletedOn = null,
                    MarketPriceId = null
                },
            };

            await dbContext.Grids.AddRangeAsync(grids);
            await dbContext.SaveChangesAsync();
        }
    }
}
