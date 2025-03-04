namespace EnergySystem.Services.Data.Projections.Property
{

    using EnergySystem.Data.Models;
    using EnergySystem.Services.Mapping;

    public class PropertyInListProjection : IMapFrom<Property>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal ElectricityNeed { get; set; }
    }
}
