namespace EnergySystem.Web.ViewModels.Property
{
    using EnergySystem.Data.Models;

    using EnergySystem.Services.Mapping;

    public class EditPropertyInputModel : BasePropertyInputModel, IMapFrom<Property>
    {
        public string Id { get; set; }
    }
}
