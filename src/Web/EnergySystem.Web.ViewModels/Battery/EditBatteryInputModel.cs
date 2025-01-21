namespace EnergySystem.Web.ViewModels.Battery
{
    using Data.Models;

    using Services.Mapping;

    using Battery=global::Battery;

    public class EditBatteryInputModel : BaseBatteryInputModel, IMapFrom<Battery>
    {
        public string Id { get; set; }
    }
}
