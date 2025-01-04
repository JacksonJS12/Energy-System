namespace EnergySystem.Services.Data.Grid
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EnergySystem.Web.ViewModels.Grid;

    public interface IGridService
    {
        Task<IEnumerable<GridViewModel
        >> GetAllGrids();
    }
}
