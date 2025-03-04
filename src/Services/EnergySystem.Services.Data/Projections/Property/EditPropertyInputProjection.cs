namespace EnergySystem.Services.Data.Projections.Property
{
    using EnergySystem.Data.Models;

    using EnergySystem.Services.Mapping;

    public class EditPropertyProjection : BasePropertyInputProjection, IMapFrom<Property>
    {
        public string Id { get; set; }
    }
}
