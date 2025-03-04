namespace EnergySystem.Services.Projections.Property
{
    using EnergySystem.Data.Models;

    using EnergySystem.Services.Mapping;

    public class EditPropertyInputProjection : BasePropertyInputProjection, IMapFrom<Property>
    {
        public string Id { get; set; }
    }
}
