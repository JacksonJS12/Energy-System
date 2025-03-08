namespace EnergySystem.Web.ViewModels.Property
{

    using System.ComponentModel;

    using EnergySystem.Data.Models;
    using EnergySystem.Services.Mapping;

    public class PropertyInListViewModel : IMapFrom<Property>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
        
        [DisplayName("Electricity need for a month")]
        public decimal ElectricityNeed { get; set; }
    }
}
