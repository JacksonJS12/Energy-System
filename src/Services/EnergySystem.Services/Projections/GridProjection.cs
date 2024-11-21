namespace EnergySystem.Services.Projections
{
    public class GridProjection
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public float MaximumCapacity { get; set; }

        public float CurrentUsage { get; set; }

        public float SupplyPrice { get; set; }

        public string Provider { get; set; }
    }
}
