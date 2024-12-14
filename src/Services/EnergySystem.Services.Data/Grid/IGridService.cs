namespace EnergySystem.Services.Data.Grid
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Projections;

    public interface IGridService
    {
        Task<IEnumerable<GridProjection>> GetAllGrids();
    }
}
