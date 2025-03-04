namespace EnergySystem.Services.Projections.Grid
{

    using EnergySystem.Data.Models;

    using Services.Mapping;

    public class GridProjection : IMapFrom<Grid>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal CurrentUsage { get; set; }

        public decimal SupplyPrice { get; set; }
    }
}
