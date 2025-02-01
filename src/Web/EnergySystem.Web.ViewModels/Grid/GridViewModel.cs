namespace EnergySystem.Web.ViewModels.Grid
{
    using Data.Models;

    using Services.Mapping;

    public class GridViewModel : IMapFrom<Grid>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal CurrentUsage { get; set; }

        public decimal SupplyPrice { get; set; }
    }
}
