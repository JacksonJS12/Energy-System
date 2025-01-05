namespace EnergySystem.Services.Data.Grid
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergySystem.Data;
    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;
    using EnergySystem.Web.ViewModels.Grid;

    public class GridService : IGridService
    {
        private readonly IDeletableEntityRepository<Grid> _gridRepository;
        public GridService(IDeletableEntityRepository<Grid> gridRepository)
        {
            this._gridRepository = gridRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            // IEnumerable<GridViewModel> allGrids = this._dbContext
            //     .Grids
            //     .Where(g => !g.IsDeleted) // Fetch only non-deleted grids
            //     .Select(g => new GridViewModel
            //     {
            //         Id = g.Id,
            //         Name = g.Name,
            //         MaximumCapacity = g.MaximumCapacity,
            //         CurrentUsage = g.CurrentUsage,
            //         SupplyPrice = g.SupplyPrice,
            //         Provider = g.Provider,
            //     })
            //     .ToArray();
            //
            // return allGrids;

            return this._gridRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList().Select(x => new KeyValuePair<string, string>(x.Id, x.Name));
        }
    }
}
