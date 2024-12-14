namespace EnergySystem.Services.Data.Grid
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EnergySystem.Data;

    using Microsoft.EntityFrameworkCore;

    using Projections;

    public class GridService : IGridService
    {
        private readonly ApplicationDbContext _dbContext;

        public GridService(ApplicationDbContext context)
        {
            this._dbContext = context;
        }

        public async Task<IEnumerable<GridProjection>> GetAllGrids()
        {
            IEnumerable<GridProjection> allGrids = await this._dbContext
                .Grids
                .Where(g => !g.IsDeleted) // Fetch only non-deleted grids
                .Select(g => new GridProjection
                {
                    Id = g.Id,
                    Name = g.Name,
                    MaximumCapacity = g.MaximumCapacity,
                    CurrentUsage = g.CurrentUsage,
                    SupplyPrice = g.SupplyPrice,
                    Provider = g.Provider,
                })
                .ToArrayAsync();

            return allGrids;
        }
    }
}
