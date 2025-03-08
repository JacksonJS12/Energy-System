namespace EnergySystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PropertyPowerPanelEntityConfiguration : IEntityTypeConfiguration<Property>
    {

        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder
                .HasOne(property => property.PowerPanel)
                .WithOne(powerPanel => powerPanel.Property)
                .HasForeignKey<Property>(property => property.PowerPanelId);
        }
    }
}
