using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class PropertyTablePropertiesUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InitialInstalation",
                table: "Batteries",
                newName: "InitialInstallation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InitialInstallation",
                table: "Batteries",
                newName: "InitialInstalation");
        }
    }
}
