using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewPropertiesToPropertyBatteryAndReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LifetimeEnergyDeliveredpe",
                table: "Batteries",
                newName: "LifetimeEnergyStoredAtStartOfDay");

            migrationBuilder.AddColumn<float>(
                name: "EnergyUsedFromGridToday",
                table: "Properties",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceChargingAt",
                table: "Batteries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnergyUsedFromGridToday",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PriceChargingAt",
                table: "Batteries");

            migrationBuilder.RenameColumn(
                name: "LifetimeEnergyStoredAtStartOfDay",
                table: "Batteries",
                newName: "LifetimeEnergyDeliveredpe");
        }
    }
}
