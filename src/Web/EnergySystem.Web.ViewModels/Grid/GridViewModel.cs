namespace EnergySystem.Web.ViewModels.Grid
{
    using EnergySystem.Data.Models;

    using EnergySystem.Services.Mapping;

    public class GridViewModel : IMapFrom<Grid>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public float MaximumCapacity { get; set; }

        public float CurrentUsage { get; set; }

        public float SupplyPrice { get; set; }

        public string Provider { get; set; }
    }
}
