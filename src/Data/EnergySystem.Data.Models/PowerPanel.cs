namespace EnergySystem.Data.Models
{
    public class PowerPanel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public virtual Property Property { get; set; }
    }
}
