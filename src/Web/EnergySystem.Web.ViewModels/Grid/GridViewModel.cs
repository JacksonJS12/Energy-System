namespace EnergySystem.Web.ViewModels.Grid
{
    using Data.Models;

    using Services.Mapping;

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
